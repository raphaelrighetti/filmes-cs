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

### Aula 3

#### GET actions

Nesta aula aprendi o que é uma "action" e criei as minhas primeiras actions que respondem à requisições GET recebendo parâmetros por meio da URL, tanto pelo caminho quanto pela query string, e vi que dá para aplicar as mesmas validações que apliquei no meu modelo "Filme" nesses parâmetros. Também apliquei o conceito de paginação no retorno da lista de filmes utilizando os métodos "Skip()" e "Take()", fazendo uma lógica que calcula o valor passado ao "Skip()" dependendo do número da página que o usuário passar no parâmetro "pagina" da URL.

Além disso, aprendi a padronizar as respostas da API utilizando o IActionResult como retorno (quando necessário) e os métodos do ControllerBase que facilitam na construção dessas respostas. Fazendo essa padronização, garantimos que as respostas das nossas actions sempre tenham o status code correto e todas as características necessárias para as possíveis respostas que iremos devolver.

### Aula 4

#### Persistindo dados com EntityFramework

Nesta aula aprendi a utilizar o EntityFramework para persistir os dados no banco de dados utilizando o "DbContext", "DbSet" e uma biblioteca específica para o MySQL, que facilita a injeção do DbContext de forma correta utilizando o método "UseMySql()" que ela disponibiliza. Fazendo isso, também aprendi como funciona a injeção de dependências no ASP.NET.

Além disso, utilizei a ferramenta de console do NuGet para gerar uma "Migration" para o meu banco de dados, o que faz com que a gente economize muito tempo que gastaríamos criando essa Migration manualmente.

Também criei um DTO para o cadastro do modelo "Filme", utilizando a biblioteca "AutoMapper" e sua extensão que dá suporte à injeção de dependências para fazer a conversão desse DTO para o modelo. Já havia utilizado o AutoMapper antes, mas não com a sua extensão de injeção de dependências. Ela facilita muito, pois além de injetar o "IMapper" em todos os recursos que precisamos dele no nosso projeto, também pega todos os "Profile" criados em nosso assembly e gera automaticamente uma configuração aplicando todos eles.

### Aula 5

#### Atualizando e removendo

Nesta aula aprendi a criar ações para os métodos HTTP PUT, PATCH e DELETE, além de ter visto pela primeira vez o PATCH na prática, achei muito interessante.

Utilizei a biblioteca "Microsoft.AspNetCore.Mvc.NewtonsoftJson" para realizar o patch no registro, utilizando a classe "JsonPatchDocument" como parâmetro vindo do corpo da requisição. Tive alguns problemas implementando a action de PATCH, mas no fim descobri que eu estava esquecendo de injetar a dependência do NewtonSoft nos meus controllers.

O mais complicado dessa aula foi justamente fazer a action de PATCH, pois nunca havia feito um método que respondesse a esse método HTTP e tive que pesquisar um pouco sobre o ModelState e outras coisas para entender o que estava acontecendo por baixo dos panos, mas isso foi bom, pois eu entendi um pouco do que acontece por baixo dos panos nas validações feitas nos objetos recebidos por parâmetro nas requisições e percebi que o que eu estava fazendo na action de PATCH era para justamente lançar uma resposta de falha na validação caso o objeto que sofreu o patch não estivesse válido, e é no ModelState que essas informações de validação ficam.
