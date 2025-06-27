# Plantilla de Servicio de Windows con Arquitectura Limpia y DDD (.NET Framework)

Este repositorio contiene una plantilla de Visual Studio para crear servicios de Windows robustos, mantenibles y escalables sobre .NET Framework. La arquitectura está diseñada siguiendo los principios de **Arquitectura Limpia (Clean Architecture)** y **Diseño Guiado por el Dominio (Domain-Driven Design - DDD)**, proporcionando una base sólida para proyectos serios.

El objetivo de esta plantilla es acelerar el desarrollo inicial, estableciendo una estructura y un conjunto de patrones que promueven un código desacoplado, testeable y fácil de evolucionar.

## Principios Arquitectónicos Clave

Esta plantilla no es solo un esqueleto de proyecto; es una implementación de una filosofía de diseño que se basa en los siguientes patrones y principios:

* **Arquitectura Limpia:** Separación estricta de capas (`Domain`, `Application`, `Infrastructure`, `Presentation`), con un flujo de dependencias que apunta siempre hacia el interior (hacia el `Domain`).
* **Domain-Driven Design (DDD):** Fomenta la creación de un modelo de dominio rico, con entidades y agregados que encapsulan la lógica y las reglas de negocio, manteniendo el corazón de la aplicación aislado de las preocupaciones técnicas.
* **Arquitectura de Cortes Verticales (Vertical Slice Architecture):** La capa de `Application` está organizada por funcionalidad o caso de uso, no por tipo de archivo. Esto maximiza la cohesión de las features y minimiza el acoplamiento entre ellas.
* **Patrón Mediador Personalizado (CQRS):** Se utiliza una implementación propia y ligera del patrón Mediador para desacoplar la invocación de la lógica de negocio, separando claramente los Comandos (escrituras) de las Consultas (lecturas).
* **Patrón Decorador para Comportamientos Transversales:** Se reemplazan los "pipeline behaviors" con decoradores para manejar de forma elegante las preocupaciones transversales como el logging, la validación, las transacciones y el caching, sin "ensuciar" los manejadores de lógica de negocio.
* **Inyección de Dependencias (DI) con Scrutor:** Configuración centralizada y automatizada de las dependencias, promoviendo el principio de "Convención sobre Configuración".
* **Logging Personalizado y Abstraído:** Incluye una implementación de un servicio de logging personalizado, demostrando cómo desacoplarse por completo de librerías de terceros y tener control total sobre la infraestructura.

## Estructura del Proyecto

La plantilla está organizada de la siguiente manera, reflejando la separación de capas:

```
Solution/
├── Domain/
│   ├── Abstractions/
│   │   └── IDomainEvent.cs
│   ├── Common/
│   │   └── Entity.cs
│   └── Convenios/  <-- Ejemplo de un Agregado de Dominio
│       ├── Convenio.cs
│       ├── ConvenioCreadoEvent.cs
│       └── IConvenioRepository.cs
│
├── Application/
│   ├── Abstractions/
│   │   ├── Logging/
│   │   │   └── ILoggingService.cs
│   │   ├── Messaging/
│   │   │   ├── ICommand.cs, IQuery.cs, IMediator.cs, ...
│   │   └── Worker/
│   │       └── IWorker.cs
│   ├── Behaviors/
│   │   └── LoggingCommandHandlerDecorator.cs
│   ├── Convenios/  <-- Slice para el caso de uso del Agregado
│   │   └── Crear/
│   │       ├── CrearConvenioCommand.cs
│   │       └── CrearConvenioCommandHandler.cs
│   └── ...
│
├── Infrastructure/
│   ├── Logging/
│   │   └── TxtLoggingService.cs
│   └── Persistence/
│       └── Convenios/
│           └── ConvenioRepositoryTxt.cs
│
└── Presentation/
    └── ParallelProcesosWorker.cs
```

## Cómo Empezar

### Prerrequisitos
* Visual Studio 2019 o superior.
* .NET Framework 4.8 (o la versión que hayas configurado).

### Crear la Plantilla
1.  **Exportar la Plantilla:** Desde Visual Studio, con la solución de la plantilla abierta, ve a `Proyecto -> Exportar plantilla...`.
2.  **Configurar:** Elige `Plantilla de proyecto`, selecciona tu proyecto principal y rellena los detalles (nombre, descripción). Asegúrate de que "Importar automáticamente la plantilla..." esté marcado.
3.  **Reiniciar Visual Studio:** Cierra y vuelve a abrir Visual Studio para que cargue la nueva plantilla.
4.  **Crear un Nuevo Proyecto:** Ve a `Archivo -> Nuevo -> Proyecto` y busca el nombre que le diste a tu plantilla. Al crear un proyecto a partir de ella, tendrás una copia limpia de esta arquitectura.

### Primeros Pasos en un Nuevo Proyecto
1.  **Modela tu Dominio:** Ve a la capa `Domain` y reemplaza o elimina el ejemplo de `Convenios` con tus propios Agregados, Entidades y Objetos de Valor.
2.  **Define tus Casos de Uso:** En la capa `Application`, crea nuevas carpetas para tus "cortes verticales", definiendo los `Commands` y `Queries` con sus respectivos `Handlers`.
3.  **Implementa la Infraestructura:** En la capa `Infrastructure`, crea las implementaciones concretas de las interfaces que definiste en el Dominio (ej. repositorios para Entity Framework, Dapper, etc.).
4.  **Conecta las Dependencias:** Ve a `DependencyContainer.cs` para registrar tus nuevos servicios (ej. `services.AddScoped<IMiRepositorio, MiRepositorioSql>();`).

## Ejecución y Despliegue

### Modo de Depuración
Para ejecutar el servicio como una aplicación de consola desde Visual Studio (ideal para depurar), simplemente presiona **`F5`** o **`Ctrl+F5`**. La lógica en `Program.cs` detectará el modo interactivo y lanzará una ventana de consola con los logs.

### Instalación como Servicio de Windows
1.  Abre un **Símbolo del sistema para desarrolladores de Visual Studio** como Administrador.
2.  Navega a la carpeta `bin/Release` de tu proyecto compilado.
3.  Ejecuta el siguiente comando para instalar el servicio:
    ```shell
    InstallUtil.exe TuServicio.exe
    ```
4.  Para desinstalarlo, usa el flag `/u`:
    ```shell
    InstallUtil.exe /u TuServicio.exe
    ```

## Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo `LICENSE.md` para más detalles.

## Autor

**Santiago Suarez Suarez** - [www.linkedin.com/in/santiago-suarez-423011171]
