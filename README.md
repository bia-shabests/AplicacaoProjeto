#  Sistema de Cadastro de Categorias - Refatoração

Este projeto é uma **refatoração completa** de um sistema que desenvolvi no início da minha jornada na tecnologia.  
O objetivo principal foi aplicar **boas práticas de Clean Code**, **padrões de camadas** e testes automatizados.

---

## **Sobre o Projeto Original**

No projeto original, criei uma API básica para gerenciar **categorias**, **subcategorias**, **produtos** e **centro de distribuição** usando **MySQL** como banco de dados.  
Era um CRUD simples, sem camadas definidas e sem testes automatizados.  
Mesmo simples, foi importante para entender **procedures**, **acesso a banco via Dapper** e conceitos iniciais de API REST.

---

## **Principais Melhorias nesta Refatoração**

- **Migração de MySQL para SQL Server Express**
- Estrutura em **camadas claras**:
  - **Controllers** → Recebem requisições e chamam os **Services**
  - **Services** → Contêm regras de negócio e validações
  - **Repositories** → Lidam com o acesso ao banco usando **Dapper**
- **Procedures** recriadas e otimizadas em SQL Server
- **Validações robustas** usando Regex
- **Testes unitários**:
  - **MSTest** para Services
  - **xUnit** para Controllers
  - **Moq** para simulação de dependências
- **Serilog** configurado para salvar logs em **tabela no SQL Server**

## Proxímas atualizações:

- **ASP.NET Core Identity** → Gerenciamento de usuários e roles
- **RabbitMQ** → Estrutura pronta para mensageria assíncrona

---

##  **Principais Tecnologias**

- **.NET 8**
- **C#**
- **Dapper**
- **SQL Server Express**
- **RabbitMQ**
- **ASP.NET Core Identity**
- **Serilog** com sink para banco de dados
- **MSTest + xUnit + Moq**

---

##  **Principais Funcionalidades**

- **Cadastrar Categoria**  
  - Validações de nome (tamanho, caracteres especiais)
  - Armazena data de criação e modificação
- **Buscar Categorias**
  - Filtros dinâmicos por ID, Nome, Status
  - Ordenação dinâmica (ASC/DESC) validada na Service
- **Logs estruturados**
  - Toda ação relevante é logada (Controller + Service)
 
## Proxímas funcionalidades:

- **Controle de Usuários**
  - Registro e Login usando Identity
  - Proteção de rotas com `[Authorize]`
- **Base para mensageria**
  - Estrutura inicial para publicar eventos em RabbitMQ

---

## **Como rodar**

**Clone o repositório:**
```bash
git clone https://github.com/bia-shabests/AplicacaoProjeto
```
- Configure sua connectionString via Secrets.
- Execute a criação da tabela e após isso as procedures na pasta DataAccess > StoredProcedures
- Execute o projeto. 
