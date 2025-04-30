
# Ualá Backend Challenge – Microblogging API

Este proyecto implementa una plataforma de microblogging similar a Twitter, permitiendo a los usuarios publicar tweets, seguir a otros usuarios y ver un timeline con los mensajes de las personas a las que siguen. Fue desarrollado con .NET 8, siguiendo principios de arquitectura limpia, CQRS y MediatR, y está totalmente containerizado y desplegado en AWS.

---

## Características

- Publicación de tweets (máx. 280 caracteres)
- Seguimiento de usuarios
- Timeline personalizado
- Separación de capas por Clean Architecture
- Patrón CQRS implementado con MediatR
- Despliegue automático en AWS Fargate vía CI/CD

---

## Tecnologías utilizadas

- **.NET 8** con ASP.NET Core
- **PostgreSQL** como base de datos relacional
- **Redis** como cache en memoria
- **CQRS + MediatR** para separación de comandos y queries
- **Docker** para contenerización
- **AWS ECS Fargate** para ejecución serverless de contenedores
- **AWS Cloud Map** para descubrimiento de servicios
- **AWS ECR** como repositorio de imágenes
- **GitHub Actions** para CI/CD
- **AWS WAF + ALB** para protección y distribución de tráfico
- **CloudWatch Logs** para centralización de logs

---

## Estructura del repositorio

```
/src
  /API                    - API REST en ASP.NET Core
  /Application            - Casos de uso (CQRS + MediatR)
  /Domain                 - Entidades de negocio
  /Infrastructure         - Repositorios, Redis, PostgreSQL, JWT, Logging

/tests
  /Application.Tests      - Tests unitarios con xUnit
  /API.Tests              - Tests de integración

/docker-compose.yml       - Entorno de desarrollo local
/docker-compose.prod.yml  - Configuración para despliegue productivo simulado
```

---

## Cómo levantar el proyecto localmente

### Requisitos

- [Docker](https://www.docker.com/) y Docker Compose instalados
- Puertos libres:
  - `8080` (API)
  - `5432` (PostgreSQL)
  - `6379` (Redis)

### Comandos

```bash
git clone https://github.com/tu-usuario/uala-backend-challenge.git
cd uala-backend-challenge
docker-compose up --build
```

La API estará disponible en:
```
http://localhost:5000
```

---

## Variables de entorno

Este proyecto no requiere configuración manual de variables de entorno para ejecutarse localmente, ya que todas las configuraciones necesarias están definidas en `docker-compose.yml` y archivos de configuración internos.

---

## Arquitectura y despliegue en AWS

La solución está desplegada en AWS utilizando los siguientes componentes:

- **GitHub Actions**: build de imagen y push a Amazon ECR
- **Amazon ECR**: repositorio de imágenes Docker
- **Amazon ECS + Fargate**: ejecución serverless de contenedores (API, Redis, PostgreSQL)
- **Application Load Balancer (ALB)**: balanceo de tráfico HTTP
- **AWS WAF**: protección contra abuso y bots, conectado al ALB
- **Cloud Map**: resolución interna de servicios (`postgres.local`, `redis.local`)
- **CloudWatch Logs**: logs centralizados por servicio
- **Security Groups**: restringen el tráfico entre servicios

---

### Diagrama de arquitectura

![Arquitectura AWS con CI/CD, WAF, ALB y Service Discovery](https://github.com/MartinezSantiago/UalaChallenge/blob/master/Diagrams/CloudArchitecture-Diagram.png)

---

## Testing

```bash
dotnet test
```

Los tests están enfocados en los casos de uso principales: creación de tweets, seguimiento de usuarios y timeline, utilizando mocks con Moq y FluentAssertions.

---

## Endpoint público (deploy)

```
http://uala-api-alb-465146145.us-east-2.elb.amazonaws.com
```

---

## Contacto

Este proyecto fue desarrollado como parte del proceso técnico de selección. Ante cualquier consulta o necesidad de aclaración técnica, quedo a disposición.
