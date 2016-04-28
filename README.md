# Semicoolon
Naamloos project voor Antwerpen.

## DB model
[LucidChart](https://www.lucidchart.com/publicSegments/view/f0ecc069-a7d9-41ea-9374-55223f55f36e "Database Model")

## API
Base URL van de API is altijd `http://semicolon.multimediatechnology.be/api/v1/`

### Login
* `POST /authenticate` (email, password)

  **When incorrect:**
  ```
  {
    "error": "invalid_credentials"
  }
  ```
  **When logged in:**
  ```
  {
    "token": "TOKENHERE"
  }
  ```
  
### Projects
* `GET /projects` returns all projects:
  ```

  ```