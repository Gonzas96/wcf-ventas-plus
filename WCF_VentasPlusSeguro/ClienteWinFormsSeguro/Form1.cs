using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClienteWinFormsSeguro.VentasServiceRef;

namespace ClienteWinFormsSeguro
{
    public partial class Form1 : Form
    {
        private List<Producto> productosDisponibles = new List<Producto>();
        private DataTable tablaDetalle = new DataTable();
        public Form1()
        {
            InitializeComponent();
            ConfigurarTablaDetalle();
            CargarProductos();

        }

        private void ConfigurarTablaDetalle()
        {
            tablaDetalle.Columns.Add("IdProducto", typeof(int));
            tablaDetalle.Columns.Add("Producto", typeof(string));
            tablaDetalle.Columns.Add("Cantidad", typeof(int));
            tablaDetalle.Columns.Add("PrecioUnitario", typeof(decimal));
            tablaDetalle.Columns.Add("Subtotal", typeof(decimal));

            dgvDetalle.DataSource = tablaDetalle;
            dgvDetalle.Columns["IdProducto"].Visible = false;
        }

        private void CargarProductos()
        {
            try
            {
                VentasServiceClient cliente = CrearCliente();
                productosDisponibles = new List<Producto>(cliente.ObtenerProductosActivos());

                dgvProductos.DataSource = null;
                dgvProductos.DataSource = productosDisponibles;

                // Ocultar columnas por índice de forma segura
                foreach (DataGridViewColumn col in dgvProductos.Columns)
                {
                    string nombre = col.Name.ToLower();
                    if (nombre == "estado" || nombre == "descripcion")
                        col.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private VentasServiceClient CrearCliente()
        {
            VentasServiceClient cliente = new VentasServiceClient();
            cliente.ClientCredentials.UserName.UserName = "adminventas";
            cliente.ClientCredentials.UserName.Password = "Ventas2026";
            return cliente;
        }

        private void ActualizarTotal()
        {
            decimal total = 0;
            foreach (DataRow row in tablaDetalle.Rows)
                total += (decimal)row["Subtotal"];

            lblTotal.Text = $"Total: S/ {total:F2}";
        }

        private decimal ObtenerTotal()
        {
            decimal total = 0;
            foreach (DataRow row in tablaDetalle.Rows)
                total += (decimal)row["Subtotal"];
            return total;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto productoSeleccionado = productosDisponibles[dgvProductos.CurrentRow.Index];

            if (cantidad > productoSeleccionado.Stock)
            {
                MessageBox.Show($"Stock insuficiente. Stock disponible: {productoSeleccionado.Stock}", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataRow row in tablaDetalle.Rows)
            {
                if ((int)row["IdProducto"] == productoSeleccionado.Id)
                {
                    MessageBox.Show("El producto ya fue agregado.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            decimal subtotal = cantidad * productoSeleccionado.Precio;

            tablaDetalle.Rows.Add(
                productoSeleccionado.Id,
                productoSeleccionado.Nombre,
                cantidad,
                productoSeleccionado.Precio,
                subtotal
            );

            ActualizarTotal();
            txtCantidad.Clear();
        }

        private void btnGenerarVenta_Click(object sender, EventArgs e)
        {
            if (tablaDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                VentasServiceClient cliente = CrearCliente();

                Venta venta = new Venta
                {
                    Fecha = DateTime.Now,
                    Total = ObtenerTotal(),
                    Estado = "A",
                    IdCliente = 1
                };

                List<DetalleVenta> detalles = new List<DetalleVenta>();
                foreach (DataRow row in tablaDetalle.Rows)
                {
                    detalles.Add(new DetalleVenta
                    {
                        IdProducto = (int)row["IdProducto"],
                        Cantidad = (int)row["Cantidad"],
                        PrecioUnitario = (decimal)row["PrecioUnitario"],
                        Subtotal = (decimal)row["Subtotal"]
                    });
                }

                cliente.RegistrarVenta(venta, detalles.ToArray());

                MessageBox.Show("¡Venta registrada exitosamente!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                tablaDetalle.Clear();
                ActualizarTotal();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar venta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    


        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dvgDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
    }
}
