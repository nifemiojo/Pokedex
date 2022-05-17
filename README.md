# Pokedex

### Run Application
Make sure Docker is intalled on machine
Application is stored in public DockerHub Registry: femiojo/pokedexapp
1. Pull down from DockerHub using `docker pull femojo/pokedexapp`
2. Run container using: `docker run -d -p 8080:80 --name pokedex femojo/pokedexapp`

Visit application at: http://localhost:8080/pokemon/mewtwo

### In a production application
- Would ensure connection was through HTTPS
- Rate limiting to avoid a malicious user from overwoking server