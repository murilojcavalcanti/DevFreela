# DevFreela

### Desafio de Aprendizagem: Dominando a Injeção de Dependência no .NET 8

Seu desafio é refatorar uma pequena funcionalidade em nossa nova aplicação de e-commerce. O código atual, embora funcional, foi escrito com um forte acoplamento entre os componentes, o que dificulta a manutenção e, principalmente, a criação de testes automatizados.

Este desafio irá guiá-lo(a) na aplicação dos princípios de Injeção de Dependência (DI) para criar um código mais limpo, desacoplado e profissional.

**Objetivo Final:**

Ao concluir o desafio, você deverá ser capaz de explicar e demonstrar:
- Como identificar e resolver problemas de acoplamento.
- Como usar interfaces para criar abstrações e injetar dependências.
- Como registrar e controlar injeções de dependência do ASP.NET Core.
- A diferença prática entre os tempos de vida de serviço: `Transient`, `Scoped` e `Singleton`.

**Vamos começar!**

---

### Fase 1: Análise do Código Inicial

Primeiro, crie um novo projeto ASP.NET Core MVC e adicione as seguintes classes e a view.

#### 1. O Modelo: `Product.cs`

Crie na pasta `Models/`:

```csharp
// Models/Product.cs
namespace DesafioDI.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

#### 2. O Repositório: `ProductRepository.cs`
Crie na pasta `Repositories/`:

```csharp
// Repositories/ProductRepository.cs
using DesafioDI.Models;

namespace DesafioDI.Repositories;

public class ProductRepository
{
    // Simula uma busca no banco de dados
    public IEnumerable<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product { Id = 1, Name = "Laptop Gamer", Price = 7500.00m },
            new Product { Id = 2, Name = "Mouse Vertical", Price = 250.50m },
            new Product { Id = 3, Name = "Teclado Mecânico", Price = 499.99m }
        };
    }
}
```

#### 3. O Controller: `ProductsController.cs`
Crie na pasta `Controllers/`:

```csharp
// Controllers/ProductsController.cs
using DesafioDI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDI.Controllers;

public class ProductsController : Controller
{
    private readonly ProductRepository _repository;

    public ProductsController()
    {
        // O Controller está criando sua própria dependência diretamente.
        _repository = new ProductRepository();
    }

    public IActionResult Index()
    {
        var products = _repository.GetProducts();
        return View(products);
    }
}
```
#### 4. A View: `Index.cshtml`
Crie na pasta `Views/Products/`:

```razor
@model IEnumerable<DesafioDI.Models.Product>

@{
    ViewData["Title"] = "Catálogo de Produtos";
}

<h1>@ViewData["Title"]</h1>

<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Preço</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var product in Model)
        {
            <tr>
                <td>@product.Id</td>
                <td>@product.Name</td>
                <td>@product.Price.ToString("C")</td>
            </tr>
        }
    </tbody>

**✅ Checkpoint da Fase 1:**

Execute o projeto. Você verá uma lista de produtos. Agora, reflita e prepare-se para responder:

1. **Quais são as desvantagens de o `ProductsController` instanciar o `ProductRepository` diretamente com `new()`?**
2. **Como você faria para testar o `ProductsController` de forma isolada, sem usar a implementação real do `ProductRepository`?**
3. **Se a classe `ProductRepository` mudasse e passasse a exigir um parâmetro em seu construtor (como uma string de conexão), que alterações você teria que fazer no `ProductsController`? Isso é sustentável?**

---

### Fase 2: A Missão de Refatoração

Sua tarefa agora é desacoplar o `ProductsController` da implementação concreta do `ProductRepository`.

**Suas Tarefas:**

1. **Crie uma Abstração:** Crie uma interface `IProductRepository` no diretório `Repositories`. Essa interface deve definir o "contrato" do que um repositório de produtos deve fazer (ou seja, deve conter a assinatura do método `GetProducts`).
2. **Implemente a Abstração:** Modifique a classe `ProductRepository` para que ela implemente a interface `IProductRepository`.
3. **Inverta a Dependência:** Refatore o `ProductsController` para que ele não dependa mais da classe `ProductRepository`, mas sim da interface `IProductRepository`. A dependência deve ser recebida (injetada) através do construtor do controller.
4. **Configure o Contêiner de DI:** No arquivo `Program.cs`, você deve "ensinar" ao ASP.NET Core como resolver essa nova dependência. Registre o serviço de forma que, sempre que um componente solicitar uma `IProductRepository`, o contêiner de DI forneça a ele uma instância da classe `ProductRepository`.

**✅ Checkpoint da Fase 2:**

Após a refatoração, a aplicação deve funcionar exatamente como antes. Prepare-se para responder:

1. **Qual é o papel da interface `IProductRepository` nesta nova arquitetura? Por que ela é tão importante?**
2. **Quem é o responsável por criar a instância de `ProductRepository` agora? O `ProductsController` ainda sabe como ela é criada?**

---

### Fase 3: Explorando os Tempos de Vida (Lifetimes)

Para aprofundar seu conhecimento, vamos experimentar os diferentes ciclos de vida que um serviço pode ter.

**Suas Tarefas:**

1.  **Crie um Serviço de Rastreamento:** Crie uma nova pasta `Services` e, dentro dela, a interface `IOperationLogger` e a classe `OperationLogger`, conforme o código abaixo. Cada instância de `OperationLogger` terá um ID único.

    // Services/IOperationLogger.cs
    public interface IOperationLogger { string OperationId { get; } }

    // Services/OperationLogger.cs
    public class OperationLogger : IOperationLogger
    {
        public string OperationId { get; private set; }
        public OperationLogger()
        {
            OperationId = Guid.NewGuid().ToString("N")[..8];
        }
    }

2.  **Injete o Logger:** Injete o `IOperationLogger` tanto no `ProductsController` quanto no `ProductRepository`.
3.  **Exiba o ID:** No método `Index` do controller, passe o `OperationId` do logger para a `View` (usando `ViewData` ou `ViewBag`) e exiba esse ID na tela.
4.  **Faça o Experimento:** Em `Program.cs`, você irá registrar o `IOperationLogger` de três formas diferentes. **Teste uma de cada vez.** Para cada teste, execute a aplicação, atualize a página várias vezes (F5) e observe atentamente o que acontece com o ID exibido na tela.

**✅ Checkpoint da Fase 3:**

Documente o que você observou. Prepare-se para responder:
1.  Descreva o comportamento do `OperationId` para cada um dos tempos de vida. O que aconteceu ao atualizar a página em cada cenário?
2.  Considerando o que você observou, em que tipo de situação prática você escolheria `AddTransient`? E `AddScoped`? E `AddSingleton`?

---
