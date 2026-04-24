🌐 MCPLab — Exemplo Básico de Servidor MCP com .NET + Ollama
<br/>Este projeto demonstra uma estrutura BÁSICA para entender como utilizar um servidor MCP (Model Context Protocol) integrado a uma API e a modelos de IA locais, como o Ollama.
<br/>O objetivo é ensinar, de forma simples e acessível, como funciona o fluxo completo:
<br/>Frontend → API → MCP → Modelo de IA → API externa (clima)
<br/>Ideal para iniciantes que querem aprender arquitetura limpa, camadas desacopladas e integração com IA.

---

## 🔗 Navegação

- [Arquitetura](arquitetura.md)
- [Como Rodar](como-rodar.md)
- [Demonstração](demonstracao.md)
- [Agradecimentos](agradecimentos.md)

---

## 🌦️ O que este projeto faz

- Consulta clima usando API gratuita  
- Envia perguntas ao Ollama  
- Demonstra fluxo MCP completo  
- Ensina arquitetura limpa e desacoplada  

---

## 🤖 Modelos gratuitos suportados

- OpenAI gpt‑4o‑mini  
- HuggingFace Inference API  
- Ollama  
- LM Studio  

---

## 🧱 Estrutura da Solution

<br/>MCPLab
<br/>│
<br/>├── Apresentacao
<br/>│     ├── MCPLab.Web (HTML puro)
<br/>│     └── MCPLab.WinForms (opcional futuramente)
<br/>│
<br/>├── API
<br/>│     └── MCPLab.Api (ASP.NET Core)
<br/>│
<br/>└── MCP
<br/>&emsp;&emsp;&emsp;&emsp;└── MCPLab.McpServer (.NET MCP Server)
