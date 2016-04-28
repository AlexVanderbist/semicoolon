# Semicoolon
Naamloos project voor Antwerpen.

## DB model
[LucidChart](https://www.lucidchart.com/publicSegments/view/f0ecc069-a7d9-41ea-9374-55223f55f36e "Database Model")

## API
Base URL van de API is altijd `http://semicolon.multimediatechnology.be/api/v1/`

### Login
* `POST /authenticate` (email, password) - Generate a token for a user.
  ```
  {
    "token": "TOKENHERE"
  }
  ```
  **When incorrect:**
  ```
  {
    "error": "invalid_credentials"
  }
  ```

* `GET /authenticate/user` (token) - Returns the logged in user.
  ```
  {
    "user": {
      "id": 1,
      "firstname": "Admin",
      "lastname": "Root",
      "email": "test@host.local",
      "created_at": null,
      "updated_at": null,
      "city": "",
      "birthyear": 0,
      "sex": 0
    }
  }
  ```
  **When incorrect:**
  ```
  {
    "error": "token_invalid" // or token_expired or token_absent
  }
  ```
  
### Projects
* `GET /projects` returns all projects:
  ```

  ```