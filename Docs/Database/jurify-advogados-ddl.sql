CREATE EXTENSION IF NOT EXISTS "pgcrypto";

drop table if exists processos_juridicos_eventos_anexos;
drop table if exists processos_juridicos_eventos;
drop table if exists processos_juridicos;
drop table if exists enderecos;
drop table if exists clientes;

create table clientes(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,

	nome varchar(100) not null,
	sobrenome varchar(200) not null,
	rg varchar(15) null,
	cpf varchar(11) null,
	email varchar(256) null,
	data_nascimento timestamp,
	
	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table clientes_anexos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_cliente uuid not null references clientes(codigo),

	nome_arquivo text not null,
	identificador text not null,
	url text not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table clientes_enderecos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_cliente uuid not null references clientes(codigo),
	
	rua varchar(300) not null,
	numero varchar(5) not null,
	cidade varchar(100) not null,
	estado varchar(50) not null,
	pais varchar(50) not null,
	cep char(8) not null,
	complemento varchar(100) null,
	observacoes varchar(1000) null,
	tipo numeric(2) not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table processos_juridicos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_advogado_responsavel uuid null,
	codigo_cliente uuid not null references clientes(codigo),

	numero varchar(30) null,
	uf varchar(2) null,
	descricao_curta varchar(100) not null,
	descricao varchar(3000) null,
	juiz_responsavel text null,
	tipo_papel numeric(2) not null,
	status numeric(2) not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);


create table processos_juridicos_eventos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_processo_juridico uuid not null references processos_juridicos(codigo),

	titulo varchar(300) not null,
	descricao varchar(3000) null,
	data_hora_evento timestamp not null default now(),

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table processos_juridicos_eventos_anexos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_evento uuid not null references processos_juridicos_eventos(codigo),

	nome varchar(200) not null,
	identificador text not null,
	url varchar(500) not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table agenda_compromissos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,
	codigo_advogado uuid not null,
	codigo_cliente uuid null references clientes(codigo),

	titulo varchar(200) not null, 
	descricao varchar(3000) null,
	data_hora_inicio_compromisso timestamp not null default now(),
	data_hora_final_compromisso timestamp null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table mensagens_recebidas(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,

	nome_cliente varchar(400) not null,
	contato_cliente varchar(30) not null,
	cpf_cliente varchar(11) not null,
	mensagem text not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create table mensagens_processos_juridicos(
	codigo uuid primary key not null default gen_random_uuid(),
	codigo_escritorio uuid not null,

	nome_cliente varchar(400) not null,
	contato_cliente varchar(30) not null,
	cpf_cliente varchar(11) not null,
	mensagem text not null,
	em_analise boolean not null default false,
	token text not null,

	data_criacao timestamp not null default now(),
	data_ultima_alteracao timestamp not null default now(),
	codigo_usuario_ultima_alteracao uuid not null,
	apagado boolean not null default false
);

create index idx_clientes_multitenancy on clientes(codigo, codigo_escritorio, apagado); 
create index idx_enderecos_multitenancy on enderecos(codigo, codigo_escritorio, apagado);
create index idx_processosjuridicos_multitenancy on processos_juridicos(codigo, codigo_escritorio, apagado);
create index idx_processosjuridicoseventos_multitenancy on processos_juridicos_eventos(codigo, codigo_escritorio, apagado);
create index idx_processosjuridicoseventosanexos_multitenancy on processos_juridicos_eventos_anexos(codigo, codigo_escritorio, apagado);