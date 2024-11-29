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
  ![image](https://github.com/user-attachments/assets/41e1cb54-36a5-45a6-9075-e35bbd44e5f7)


  - En el administrador de paquetes, seleccione el proyecto Infraestructure (capa que gestiona el acceso a los datos)
   ![image](https://github.com/user-attachments/assets/04705b2a-1cc5-4ca2-b92c-fe4b6f72fbff)


3. **Generación de Migraciones**
  - Ejecute los scripts `add-migration` y `update-migration` para generar las migraciones correspondientes
  - Asegúrese de que en la base de datos figuren las entidades propuestas en el ejercicio

4. **Ejecución del Proyecto**
  - Haga clic en HTTPS
  ![image](https://github.com/user-attachments/assets/1434abc7-d2a2-4c75-aa6e-15da4eb99676)

  - Aparecerá el navegador donde podrá visualizar Swagger y ejecutar las pruebas propuestas en el desafío
  ![image](https://github.com/user-attachments/assets/d06c7f1e-5456-44ae-86ba-3a3ce0ab4335)



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
 - Borrado fìsico del producto

5. **Active**
  - Traerá los productos existentes activos

### Consideraciones de Diseño

La relación entre entidades es 1 a 1 porque se consideró para este desafío que el producto se identifica con un código de barras, para asegurar así que la base de datos mantenga su coherencia.

## Despedida

Sin más que agregar, solo queda agradecer por considerarme como parte del proceso.

¡Saludos y buen año!
