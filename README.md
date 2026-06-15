# 🛒 VentasPlus — WCF Service + Cliente Windows Forms

Sistema de ventas desarrollado con **WCF (Windows Communication Foundation)** y un cliente de escritorio en **Windows Forms**, ambos sobre **.NET Framework 4.7.2**.

## 📌 Descripción

El proyecto implementa una arquitectura orientada a servicios (SOA) donde el servicio WCF expone operaciones de negocio que el cliente consume a través de un proxy generado automáticamente.

## ⚙️ Funcionalidades

- 📦 **Consulta de productos** — lista los productos disponibles con su información desde el servicio
- 🧾 **Registro de ventas** — permite registrar una venta a través del cliente Windows Forms, enviando los datos al servicio WCF

## 🏗️ Arquitectura

```
wcf-ventas-plus/
├── WCF_VentasPlusSeguro/     # Servicio WCF (.NET Framework 4.7.2)
│   ├── IServicioVentas.cs    # Contrato del servicio (interfaz)
│   ├── ServicioVentas.svc    # Implementación del servicio
│   └── Web.config            # Configuración de endpoints y bindings
│
└── ClienteVentasPlus/        # Cliente Windows Forms (.NET Framework 4.7.2)
    ├── Form1.cs              # Interfaz gráfica principal
    └── App.config            # Configuración del proxy WCF
```

## 🛠️ Tecnologías

| Tecnología | Uso |
|---|---|
| C# | Lenguaje principal |
| WCF (Windows Communication Foundation) | Exposición de servicios SOAP |
| Windows Forms | Interfaz gráfica del cliente |
| .NET Framework 4.7.2 | Plataforma base |
| Visual Studio | IDE de desarrollo |

## 🚀 Cómo ejecutar

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

4. Luego ejecuta **ClienteVentasPlus** — el cliente se conectará automáticamente al servicio

## 📡 Conceptos aplicados

- Contratos de servicio con `[ServiceContract]` e `[OperationContract]`
- Configuración de **bindings** y **endpoints** en `Web.config`
- Generación de proxy cliente con **"Agregar referencia de servicio"**
- Comunicación **SOAP** entre cliente y servidor
- Separación de responsabilidades: lógica en el servicio, presentación en el cliente

---

> Proyecto desarrollado como ejercicio académico para la materia de Arquitectura de Software.
