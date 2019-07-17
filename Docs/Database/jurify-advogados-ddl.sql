CREATE EXTENSION IF NOT EXISTS "pgcrypto";

create table clientes(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,

	nome varchar(100) not null,
	sobrenome varchar(200) not null,
	data_nascimento timestamp not null,
	
	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table enderecos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_cliente uuid not null references clientes(codigo),
	
	rua varchar(300) not null,
	numero varchar(5) not null,
	cidade varchar(100) not null,
	estado varchar(5) not null,
	pais varchar(50) not null,
	cep char(8) not null,
	complemento varchar(100) not null,
	observacoes varchar(1000) not null,
	tipo numeric(2) not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table casos_juridicos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_advogado_responsavel uuid not null,
	codigo_cliente uuid not null references clientes(codigo),

	descricao_curta varchar(100) not null,
	descricao varchar(3000) not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);


create table casos_juridicos_eventos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_caso_juridico uuid not null references casos_juridicos(codigo),

	descricao varchar(3000) not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table casos_juridicos_eventos_anexos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_evento uuid not null references casos_juridicos_eventos(codigo),

	nome varchar(200) not null,
	url varchar(500) not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);