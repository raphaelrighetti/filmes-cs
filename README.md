# Filmes API

"Filmes" é uma API desenvolvida ao longo dos cursos da formação "ASP.Net Core: crie aplicações com C#, .NET, Entity Framework e LINQ" da Alura, com objetivo de aprender a utilizar o ASP.NET para a criação aplicações web. É uma API que irá funcionar como um catálogo de filmes, com todo um CRUD e etc.

Também irei registrar o meu progresso no curso neste README, mostrando o que aprendi nas aulas e destacando os pontos mais importantes.

Minhas anotações feitas durante o curso podem ser achadas no arquivo "anotacoes.txt" na raiz do repositório.

## .NET 6: criando uma web API

Neste curso irei aprender o básico de como desenvolver uma API REST utilizando o ASP.NET com a linguagem C#.

### Aula 1

#### Conceito de API e REST

Nesta aula foi mostrado o que é uma API, para que serve, quais são as suas vantagens e um pouco sobre o padrão de arquitetura REST.

Não foi mostrado nada que eu não havia visto anteriormente, mas aprendi a criar um projeto de API que usa o ASP.NET.

### Aula 2

#### O primeiro controlador

Nesta aula fiz o meu primeiro controlador, um controlador do endpoint "filme", que, por enquanto, só aceita requisições com o método HTTP POST e mostra no console o objeto "Filme" recebido do corpo da requisição.

É muito interessante ver como o ASP.NET é parecido com o Spring em algumas coisas, tive bastante facilidade de entender o funcionamento do controlador que fiz. Acho que o mais diferente que notei até agora foi o jeito com que fazemos a aplicação rodar no arquivo "Program.cs", que precisa de muito mais linhas de código do que no Spring, o que não é necessariamente ruim, pois pode facilitar na hora de mudarmos alguns comportamentos da nossa aplicação, eu imagino.

Além de aprender a criar um controlador utilizando algumas anotações e estendendo a classe "ControllerBase", aprendi a fazer a validação dos campos recebidos no nosso modelo, que no meu caso é o "Filme", utilizando as anotações do namespace "System.ComponentModel.DataAnnotations".

Gostei bastante de como os controladores no ASP.NET funcionam, principalmente por serem muito similares aos controladores do Spring, que já estou acostumado a utilizar. Estou animado para continuar aprendendo.
