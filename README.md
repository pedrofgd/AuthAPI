# API para cadastro e login de usuários

### Funcionalidades

- [x] Cadastrar usuário
- [x] Login (autenticação)
- [x] Editar perfil (username, alterar senha...)
- [x] Excluir cadastro
- [x] Alterar `Role` de um usuário (permitido para `admins`)

### Rotas

* `POST /v1/account/register` - Registrar novo usuário
* `POST /v1/account/login` - Autenticação de usuários
* `PUT /v1/settings/security` - Alterar senha
* `PUT /v1/settings/profile` - Alterar dados (Username e Nome)
* `DELETE /v1/settings/:id` - Deletar conta
* `PUT /v1/management/:id` - Alterar `Role` de um usuário (permitido para `admins`)
