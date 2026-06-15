# WCF VentasPlus Seguro

Sistema de ventas implementado con **WCF (Windows Communication Foundation)** publicado en IIS, con seguridad mediante autenticacion por usuario y contrasena, y un cliente de escritorio en **Windows Forms**. Ambos desarrollados en **.NET Framework 4.7.2** con **C#**.

Desarrollado como evaluacion del curso Desarrollo de Aplicaciones Avanzado — ISIL 2026-10.

## Descripcion

El sistema permite consultar productos activos y registrar ventas de forma transaccional. Cada venta valida el stock de todos los productos antes de confirmar cualquier operacion; si algun producto no tiene stock suficiente, la transaccion se cancela completamente y no se escribe nada en la base de datos.

El servicio WCF esta protegido con autenticacion Basic sobre HTTP, implementada con un validador de credenciales personalizado (`UserNamePasswordValidator`).

## Operaciones del servicio

| Operacion | Descripcion |
|---|---|
| `ObtenerProductosActivos()` | Retorna la lista de productos con estado `'A'` desde la BD |
| `RegistrarVenta(Venta, List<DetalleVenta>)` | Registra la venta, sus detalles y descuenta el stock. Opera bajo transaccion SQL |

## Arquitectura por capas

```
WCF_VentasPlusSeguro/
|
+-- WCF_VentasPlusSeguroWS/       # Capa de servicio WCF
|   +-- IVentasService.cs         # Contrato del servicio [ServiceContract]
|   +-- VentasWS.cs               # Implementacion del contrato
|   +-- Autenticar.cs             # Validador de credenciales (UserNamePasswordValidator)
|   +-- App.config                # Configuracion de binding, endpoint y seguridad
|
+-- BL_VentasPlus/                # Capa de logica de negocio
|   +-- VentaBL.cs                # Validacion de stock y control de transaccion SQL
|   +-- ProductoBL.cs             # Delegacion a DAO para consulta de productos
|
+-- DAO_VentasPlus/               # Capa de acceso a datos
|   +-- VentaDAO.cs               # INSERT en Venta, DetalleVenta y UPDATE de stock
|   +-- ProductoDAO.cs            # SELECT de productos activos
|
+-- Model_VentasPlus/             # Entidades del dominio [DataContract]
|   +-- Producto.cs
|   +-- Venta.cs
|   +-- DetalleVenta.cs
|
+-- DB_VentasPlus/                # Gestion de conexion (patron Singleton)
    +-- DBManager.cs

ClienteWinFormsSeguro/            # Cliente Windows Forms
    +-- Form1.cs                  # UI: carga productos, arma detalle y genera venta
    +-- App.config                # Endpoint apuntando al servicio publicado en IIS
```

## Modelo de base de datos

```
Producto          Venta               DetalleVenta
--------          -----               ------------
id (PK)           id (PK)             id (PK)
codigoSKU         fecha               cantidad
nombre            total               precioUnitario
descripcion       estado              subtotal
stock             idCliente           idVenta (FK -> Venta)
precio                                idProducto (FK -> Producto)
estado
```

## Flujo de una venta

1. El cliente carga los productos activos al iniciar (`ObtenerProductosActivos`)
2. El usuario selecciona productos, ingresa cantidades y arma el detalle
3. Al presionar "Generar Venta", el cliente llama a `RegistrarVenta` enviando la venta y sus detalles
4. El servicio delega a `VentaBL`, que abre una conexion y una transaccion SQL
5. Se valida el stock de cada producto; si alguno es insuficiente, se hace `Rollback` y se lanza excepcion
6. Si todo es correcto: se inserta en `Venta`, se insertan los `DetalleVenta` y se descuenta el stock con `Commit`

## Seguridad

- **Binding**: `basicHttpBinding` con `security mode="TransportCredentialOnly"`
- **Autenticacion**: `clientCredentialType="Basic"` con validador personalizado (`Autenticar.cs`)
- **Credenciales**: usuario `adminventas`, contrasena `Ventas2026`
- El cliente inyecta las credenciales via `ClientCredentials.UserName` antes de cada llamada

## Tecnologias

| Tecnologia | Uso |
|---|---|
| C# / .NET Framework 4.7.2 | Lenguaje y plataforma base |
| WCF | Exposicion del servicio SOAP con seguridad |
| Windows Forms | Interfaz grafica del cliente |
| SQL Server | Base de datos relacional |
| ADO.NET | Acceso a datos con `SqlConnection` y `SqlTransaction` |
| IIS | Publicacion del servicio en servidor web local |

## Requisitos para ejecutar

- Visual Studio 2019 o superior
- .NET Framework 4.7.2
- SQL Server (con la base de datos `VentaPlusDB` creada)
- IIS con el servicio `WCF_VentasPlusSeguroWS` publicado

## Pasos

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Gonzas96/wcf-ventas-plus.git
   ```
2. Ejecuta los scripts SQL para crear las tablas `Producto`, `Venta` y `DetalleVenta`
3. Publica `WCF_VentasPlusSeguroWS` en IIS y verifica el endpoint en `App.config` del cliente
4. Abre `ClienteWinFormsSeguro` en Visual Studio y ejecuta
