# Arquitetura do MCPLab

A arquitetura segue o fluxo recomendado para aplicações modernas com IA:

[Frontend HTML]
		↓
[API ASP.NET Core]
		↓
[MCP Server (.NET)]
		↓
[Ollama / Azure OpenAI / API Externa]


## Por que esse é o jeito certo

- O navegador não precisa conhecer MCP  
- A API centraliza segurança e logs  
- O MCP pode evoluir sem quebrar o frontend  
- Você pode trocar o modelo de IA sem alterar nada  
