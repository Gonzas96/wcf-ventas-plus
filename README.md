# VentasPlus — WCF Service + Cliente Windows Forms

Sistema de ventas desarrollado con **WCF (Windows Communication Foundation)** y un cliente de escritorio en **Windows Forms**, ambos sobre **.NET Framework 4.7.2**.

## Descripcion

El proyecto implementa una arquitectura orientada a servicios (SOA) donde el servicio WCF expone operaciones de negocio que el cliente consume a traves de un proxy generado automaticamente.

## Funcionalidades

- **Consulta de productos** — lista los productos disponibles con su informacion desde el servicio
- **Registro de ventas** — permite registrar una venta a traves del cliente Windows Forms, enviando los datos al servicio WCF

## Arquitectura

```
wcf-ventas-plus/
├── WCF_VentasPlusSeguro/     # Servicio WCF (.NET Framework 4.7.2)
│   ├── IServicioVentas.cs    # Contrato del servicio (interfaz)
│   ├── ServicioVentas.svc    # Implementacion del servicio
│   └── Web.config            # Configuracion de endpoints y bindings
│
└── ClienteVentasPlus/        # Cliente Windows Forms (.NET Framework 4.7.2)
    ├── Form1.cs              # Interfaz grafica principal
    └── App.config            # Configuracion del proxy WCF
```

## Tecnologias

| Tecnologia | Uso |
|---|---|
| C# | Lenguaje principal |
| WCF (Windows Communication Foundation) | Exposicion de servicios SOAP |
| Windows Forms | Interfaz grafica del cliente |
| .NET Framework 4.7.2 | Plataforma base |
| Visual Studio | IDE de desarrollo |

## Como ejecutar

### Requisitos
- Visual Studio 2019 o superior
- .NET Framework 4.7.2
- IIS Express (incluido con Visual Studio)

### Pasos

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Gonzas96/wcf-ventas-plus.git
   ```

2. Abre ambas soluciones en Visual Studio (`WCF_VentasPlusSeguro` y `ClienteVentasPlus`)

3. Ejecuta primero el proyecto **WCF_VentasPlusSeguro** — esto levanta el servicio en IIS Express

4. Luego ejecuta **ClienteVentasPlus** — el cliente se conectara automaticamente al servicio

## Conceptos aplicados

- Contratos de servicio con `[ServiceContract]` e `[OperationContract]`
- Configuracion de **bindings** y **endpoints** en `Web.config`
- Generacion de proxy cliente con **"Agregar referencia de servicio"**
- Comunicacion **SOAP** entre cliente y servidor
- Separacion de responsabilidades: logica en el servicio, presentacion en el cliente

---

> Proyecto desarrollado como ejercicio academico para la materia de Arquitectura de Software.
