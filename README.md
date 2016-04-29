# Semicoolon
Naamloos project voor Antwerpen.

## DB model
[LucidChart](https://www.lucidchart.com/publicSegments/view/f0ecc069-a7d9-41ea-9374-55223f55f36e "Database Model")

## API
Base URL van de API is altijd `http://semicolon.multimediatechnology.be/api/v1/`

### Login
* #### `POST /authenticate` (email, password) - Generate a token for a user.
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

* #### `GET /authenticate/user` (token) - Returns the logged in user.
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
* #### `GET /projects` (token) returns all projects:
  ```
	{
	  "projects": [
	    {
	      "id": 1,
	      "name": "Heraanleg groenplaats",
	      "lat": "51.2194101000000",
	      "lng": "4.4010925000000",
	      "locationText": "Groenplaats",
	      "project_creator": "2",
	      "created_at": "2016-04-28 16:26:09",
	      "updated_at": "2016-04-28 16:26:09",
	      "theme_id": "1",
	      "theme": {
	        "id": 1,
	        "name": "pleinen",
	        "hex_color": "#804040",
	        "created_at": "2016-04-28 16:25:46",
	        "updated_at": "2016-04-28 16:25:46"
	      },
	      "creator": {
	        "id": 2,
	        "firstname": "Ruben",
	        "lastname": "De Swaef",
	        "email": "rubendeswaef@gmail.com",
	        "created_at": "2016-04-28 16:24:50",
	        "updated_at": "2016-04-28 16:24:50",
	        "city": "Sint-Job-In't-Goor",
	        "birthyear": "1990",
	        "sex": "0"
	      }
	    },
	    {
	      ...
	    }
	  ]
	}
  ```