# FarmacityTest

# Desafío Farmacity - Instrucciones de Implementación

## Introducción

Hola, les dejo a continuación las instrucciones relacionadas al desafío planteado:

### Pasos de Configuración

1. **Clonación del Repositorio**
  - Clone el repositorio
  - En `appsetting.json` dentro del proyecto FarmacityTest, modifique en ConnectionStrings el nombre del servidor y base de datos local para asegurar el funcionamiento del proyecto

2. **Preparación para Migraciones**
  - Seleccione el compilador en HTTPS para proceder a generar las migraciones
    ![image](https://github.com/user-attachments/assets/344e62e2-0f0f-4126-b4af-96ede1e6e824)

  - En el administrador de paquetes, seleccione el proyecto Infraestructure (capa que gestiona el acceso a los datos)
  ![image](https://github.com/user-attachments/assets/ef64c8a6-d05b-4abe-b510-acbf9b7748ad)

3. **Generación de Migraciones**
  - Ejecute los scripts `add-migration` y `update-migration` para generar las migraciones correspondientes
  - Asegúrese de que en la base de datos figuren las entidades propuestas en el ejercicio

4. **Ejecución del Proyecto**
  - Haga clic en HTTPS
     ![image](https://github.com/user-attachments/assets/344e62e2-0f0f-4126-b4af-96ede1e6e824)
  - Aparecerá el navegador donde podrá visualizar Swagger y ejecutar las pruebas propuestas en el desafío
    ![image](https://github.com/user-attachments/assets/52eaaf1c-db51-4e37-899c-eb728be87141)


### Endpoints Disponibles

Se encontrarán con 5 endpoints:

1. **All**
  - Traerá todos los productos existentes en la base de datos

2. **Get/Id**
  - Traerá particularmente el producto por el ID que le asigna la base de datos

3. **CreateOrUpdate**
  - Creará o actualizará los productos nuevos o que existan en la base de datos
  - **Importante**: Al crear, asegure que el ID del producto y el ID del código de barras sea 0

4. **Delete/Id**
  - Realizará un borrado lógico
  - Pasará los productos y su código de barra a `IsActive = false`

5. **Active**
  - Traerá los productos existentes activos (no borrados con Delete)

### Consideraciones de Diseño

La relación entre entidades es 1 a 1 porque se consideró para este desafío que el producto se identifica con un código de barras, para asegurar así que la base de datos mantenga su coherencia.

## Despedida

Sin más que agregar, solo queda agradecer por considerarme como parte del proceso.

¡Saludos y buen año!
