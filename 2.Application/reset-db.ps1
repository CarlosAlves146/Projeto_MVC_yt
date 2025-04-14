# Opção 1: Habilitar a execução apenas na sessão atual (seguro)
# No PowerShell, execute:
# Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
# Depois, execute o script normalmente:
# .\reset-db.ps1


# Apaga o banco
sqlcmd -S localhost\SQLEXPRESS -Q "DROP DATABASE MeuBanco"

# Apaga as migrations
Remove-Item -Recurse -Force Migrations

# Cria a migration inicial
dotnet ef migrations add Initial

# Cria o banco
dotnet ef database update
