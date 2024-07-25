# WEBAPI - Locadora de VeIculos

Uma locadora de ve ́ıculos precisa gerenciar uma variedade de informac ̧  ̃oes, como
cadastro de ve ́ıculos, clientes, reservas, entre outras, de forma eficiente e segura. Al ́em
disso, a implementac ̧  ̃ao desse sistema proporcionou uma oportunidade pr ́atica de aplicar
os conhecimentos adquiridos na disciplina, desde a modelagem do banco de dados at ́e a
implementac ̧  ̃ao do backend.
O sistema de locadora de ve ́ıculos desenvolvido neste projeto oferecer ́a diversas
funcionalidades, desde o cadastro de ve ́ıculos e clientes at ́e a realizac ̧  ̃ao e gest ̃ao de reser-
vas e manutenc ̧  ̃ao de ve ́ıculos. Para isso, ser ̃ao utilizadas tecnologias modernas, como C#,
Entity Framework para mapeamento objeto-relacional, SQL Server Express como banco
de dados, Angular para desenvolvimento do FrontEnd e Swagger para documentac ̧  ̃ao e
testes das APIs.

![telaHome](https://github.com/user-attachments/assets/46b79f2c-b7a4-4a7e-bd94-829b08dd4f46)
![telaReservasPreenchida](https://github.com/user-attachments/assets/448421af-86ac-4cc5-9920-482f09bc2d65)

## Implementação Técnica
O desenvolvimento do backend da aplicação foi realizado utilizando a linguagem de programação C#, escolhida por seus recursos modernos e eficientes que facilitaram a criação de aplicações web robustas e de alto desempenho. O Entity Framework Core foi utilizado como ORM (Object-Relational Mapper) para simplificar o mapeamento entre classes da aplicação e tabelas do banco de dados relacional, permitindo trabalhar com objetos em vez de comandos SQL diretamente.

Para o armazenamento e gerenciamento dos dados da aplicação, foi utilizado o SQL Server Express, uma edição gratuita e de fácil utilização do Microsoft SQL Server. 

Para a documentação da API, foi utilizado o Swagger, uma ferramenta que fornece uma interface interativa para a visualização e teste dos endpoints da API. O Swagger facilita a comunicação entre desenvolvedores e outras partes interessadas, fornecendo:

Documentação Automatizada: Gerando documentação em tempo real baseada no código-fonte da API.

Interface de Usuário: Permitindo interagir com os endpoints da API diretamente pelo navegador, facilitando a execução de testes e validação das funcionalidades.

Para o desenvolvimento do front-end, foi utilizado o framework Angular, que oferece uma forma mais eficiente e estruturada de construir os componentes de um sistema. Angular é um framework de desenvolvimento web baseado em TypeScript, criado pelo Google, que facilita a criação de aplicações de página única (SPAs) por meio de uma arquitetura modular, permitindo a reutilização de código e a manutenção facilitada.

Para complementar o Angular e tornar a estilização do sistema mais prática, foi utilizado o framework Bootstrap. Bootstrap é uma biblioteca de código aberto que fornece uma coleção de ferramentas e componentes pré-desenhados para a criação de interfaces responsivas e modernas. Desenvolvido inicialmente pelo Twitter, o Bootstrap inclui uma variedade de componentes HTML, CSS e JavaScript que simplificam a construção de layouts e a aplicação de estilos consistentes em todo o projeto.



## Instruções de Uso

### Back End
Antes de executar a API back end é necessário primeiramente alterar a string de conexão com o banco localizado no arquivo "appsettings.json", adicionando o nome de acordo com o seu banco sql local.
![image](https://github.com/ICEI-PUC-Minas-PCO-SI/pco-dad-2024-1-back-bd-ThaisAlvesSilva/assets/72041841/e26a8a36-6f8e-4f76-8f13-e22d78265807)

### Front End
* Antes de começar, certifique-se de ter o Node.js instalado na máquina.
* Após instalar o Node.js, será necessário instalar as dependências do projeto. Para isso navegue até a raiz do projeto angular e execute o comando
*     npm install
* Após a instalação das dependências, use o comando abaixo para executar o projeto:
*     ng serve
* Por padrão, o servidor estará disponível no endereço http://localhost:4200/. Abra o navegador e acesse essa URL para ver o projeto em execução.
