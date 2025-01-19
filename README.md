# SigmaTest

# Setup Instructions for Visual Studio

1. Clone the repository:
   ```bash
   git clone https://github.com/rblucky3/SigmaTest.git

   ```
2. Open the solution in Visual Studio 
3. The database will be automatically created and migrated the first time you run the application.
    Just press F5 or Ctrl+F5 
	
4. The API will be available at http://localhost:7164 by default.
5. You can interact with the API through Swagger UI at https://localhost:7164/swagger/index.html.


### Summary

By following these steps, you ensure that no additional setup is required when others clone and run your project for the first time in **Visual Studio**. This includes:
- Automatically applying database migrations.
- Configuring default connection strings and dependencies.
- Setting up a straightforward project structure for easy running.

This setup is ideal for development environments, but remember to manually handle migrations in production environments to avoid any unintentional database changes.