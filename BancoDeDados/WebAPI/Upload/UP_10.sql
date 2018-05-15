CREATE SCHEMA carona;

CREATE TABLE carona.instituicao (
	instituicao_id VARCHAR(36) NOT NULL,
	nome VARCHAR(100) NOT NULL,
	data_inclusao TIMESTAMP NOT NULL,
	data_alteracao TIMESTAMP NOT NULL,
	padrao BOOLEAN DEFAULT false NOT NULL,
	CONSTRAINT pk_instituicao PRIMARY KEY (instituicao_id)
);

CREATE TABLE carona.instituicao_polo
(
	polo_id VARCHAR(36) NOT NULL,
	instituicao_id VARCHAR(36) NOT NULL,
	nome varchar(100) not null,
	endereco varchar(100) not null,
	numero varchar(20) null,
	complemento varchar(30),
	bairro varchar(60) null,
	cidade varchar(60) null,
	uf character(2) null,
	longitude numeric(15, 6),
	latitude numeric(15, 6),
	constraint pk_polo primary key (polo_id),
	constraint fk_polo_instituicao foreign key(instituicao_id) references carona.instituicao (instituicao_id)
);
--COMMENT ON TABLE public.instituicao IS 'É a Faculdade(universidade) que o usuario irá se digir ao (ou voltar) ao solicitar a Carona';


CREATE TABLE carona.situacao (
                situacao_id INTEGER NOT NULL,
                descricao VARCHAR(100) NOT NULL,
                CONSTRAINT pk_situacao PRIMARY KEY (situacao_id)
);

COMMENT ON TABLE carona.situacao IS '1 - Ativo
2 - Bloqueado
3 - Inativo
4 - Excluido';


CREATE TABLE carona.usuario (
                usuario_id VARCHAR(36) NOT NULL,
                instituicao_id varchar(36) not null,
                usuario VARCHAR(100) NOT NULL,
                senha VARCHAR(50) NOT NULL,
                nome VARCHAR(100) NOT NULL,
                email VARCHAR(100),
                telefone VARCHAR(20),
                curso VARCHAR(50),
                data_inclusao TIMESTAMP NOT NULL,
                data_alteracao TIMESTAMP NOT NULL,
                situacao_id INTEGER NOT NULL,
                CONSTRAINT pk_usuario PRIMARY KEY (usuario_id),
                constraint fk_usuario_instituicao foreign key (instituicao_id) references carona.instituicao (instituicao_id),
                constraint fk_usuario_situacao foreign key (situacao_id) references carona.situacao (situacao_id)
);
COMMENT ON TABLE carona.usuario IS 'Usuario do Sistema';
COMMENT ON COLUMN carona.usuario.usuario IS 'email ou (usuario) deverá ser o mesmo usuario da ftec';
COMMENT ON COLUMN carona.usuario.curso IS 'É o curso que o usuario esta cursando, por exemplo ADS(Analise e desemvolvimento de sistema)';



CREATE TABLE carona.endereco (
                endereco_id VARCHAR(36) NOT NULL,
		usuario_id VARCHAR(36) NOT NULL,
                nome VARCHAR(50) NOT NULL,
                endereco VARCHAR(100),
                numero varchar(20) null,
		complemento varchar(30),
		bairro varchar(60) null,
		cidade varchar(60) null,
		uf character(2) null,
		longitude numeric(15, 6),
		latitude numeric(15, 6),
                instituicao_id VARCHAR(36) not null,
                CONSTRAINT pk_endereco PRIMARY KEY (endereco_id),
                CONSTRAINT fk_endereco_usuario foreign key (usuario_id) references carona.usuario (usuario_id)
);
COMMENT ON TABLE carona.endereco IS 'São os Endereços que serão salvos no sistema o endereço pode ser de um usuario ou pode ser de uma instituição, o usuário pode ter mais de um endereço salvo pois ele pode optar por ter o endereço de casa e do trabalho por exemplo.';
COMMENT ON COLUMN carona.endereco.endereco IS 'Na verdade nao é esse campo as outras colunas ainda serão definidas';


CREATE TABLE carona.veiculo (
                veiculo_id VARCHAR(36) NOT NULL,
                nome VARCHAR(100) NOT NULL,
                marca VARCHAR(30) NOT NULL,
                placa VARCHAR(7) NOT NULL,
                cor VARCHAR(20) NOT NULL,
                data_inclusao TIMESTAMP NOT NULL,
                data_alteracao TIMESTAMP NOT NULL,
                usuario_id VARCHAR(36) NOT NULL,
                situacao_id INTEGER NOT NULL,
                CONSTRAINT veiculo_id PRIMARY KEY (veiculo_id)
);





CREATE TABLE carona.motivo_cancelamento (
                motivo_cancelamento_id VARCHAR NOT NULL,
                descricao VARCHAR NOT NULL,
                CONSTRAINT motivo_cancelamento_id PRIMARY KEY (motivo_cancelamento_id)
);
COMMENT ON TABLE public.motivo_cancelamento IS 'são os motivos de cancelamento já predefinidos do sistema';


CREATE TABLE public.carona_status (
                carona_status_id VARCHAR NOT NULL,
                descricao VARCHAR NOT NULL,
                CONSTRAINT carona_status_id PRIMARY KEY (carona_status_id)
);
COMMENT ON TABLE public.carona_status IS 'Responsavel por armazenar os possíveis status de uma carona podendo ser

1 - Aguardando Carona
2 - Oferecendo Carona
3 - Aguardando Motorista
4 - Concluida
5 - Cancelada';






CREATE TABLE public.veiculo (
                veiculo_id VARCHAR NOT NULL,
                nome VARCHAR NOT NULL,
                marca VARCHAR NOT NULL,
                placa VARCHAR NOT NULL,
                cor VARCHAR NOT NULL,
                data_inclusao TIMESTAMP NOT NULL,
                data_alteracao TIMESTAMP NOT NULL,
                usuario_id VARCHAR NOT NULL,
                situacao_id VARCHAR NOT NULL,
                CONSTRAINT veiculo_id PRIMARY KEY (veiculo_id)
);




CREATE TABLE public.carona (
                carona_id VARCHAR NOT NULL,
                usuario_id VARCHAR NOT NULL,
                endereco_id_inicio VARCHAR NOT NULL,
                endereco_id_fim VARCHAR NOT NULL,
                motorista BOOLEAN DEFAULT false NOT NULL,
                ida BOOLEAN DEFAULT false NOT NULL,
                carona_status_id VARCHAR NOT NULL,
                motivo_cancelamento_id VARCHAR,
                motivo_cancelamento_outro VARCHAR,
                data_inclusao TIMESTAMP NOT NULL,
                data_alteracao TIMESTAMP NOT NULL,
                CONSTRAINT carona_id PRIMARY KEY (carona_id)
);
COMMENT ON TABLE public.carona IS 'Responsavel por armazenar os dados das caronas é a principal tabela do sistema';


CREATE TABLE public.avaliacao (
                avaliacao_id VARCHAR NOT NULL,
                usuario_id_avaliador VARCHAR NOT NULL,
                usuario_id_avaliado VARCHAR NOT NULL,
                nota INTEGER NOT NULL,
                data_inclusao TIMESTAMP NOT NULL,
                data_alteracao TIMESTAMP NOT NULL,
                situacao_id VARCHAR NOT NULL,
                carona_id VARCHAR NOT NULL,
                CONSTRAINT avaliacao_id PRIMARY KEY (avaliacao_id)
);
COMMENT ON TABLE public.avaliacao IS 'Responsável por guardar a informação referentes as avaliacoes dos usuarios';


CREATE TABLE public.versao_app (
                versao_id VARCHAR NOT NULL,
                versao INTEGER NOT NULL,
                data_inclusao TIMESTAMP NOT NULL,
                data_alteracao TIMESTAMP NOT NULL,
                situacao_id VARCHAR NOT NULL,
                CONSTRAINT versao_app_id PRIMARY KEY (versao_id)
);
COMMENT ON TABLE public.versao_app IS 'guarda o registro para controle de versão do aplicativo';


ALTER TABLE public.carona ADD CONSTRAINT motivo_cancelamento_carona_fk
FOREIGN KEY (motivo_cancelamento_id)
REFERENCES public.motivo_cancelamento (motivo_cancelamento_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.carona ADD CONSTRAINT carona_status_carona_fk
FOREIGN KEY (carona_status_id)
REFERENCES public.carona_status (carona_status_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.endereco ADD CONSTRAINT instituicao_endereco_fk
FOREIGN KEY (instituicao_id)
REFERENCES public.instituicao (instituicao_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.usuario ADD CONSTRAINT situacao_usuario_fk
FOREIGN KEY (situacao_id)
REFERENCES public.situacao (situacao_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.versao_app ADD CONSTRAINT situacao_versao_app_fk
FOREIGN KEY (situacao_id)
REFERENCES public.situacao (situacao_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.veiculo ADD CONSTRAINT situacao_veiculo_fk
FOREIGN KEY (situacao_id)
REFERENCES public.situacao (situacao_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.avaliacao ADD CONSTRAINT situacao_avaliacao_fk
FOREIGN KEY (situacao_id)
REFERENCES public.situacao (situacao_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.endereco ADD CONSTRAINT usuario_endereco_fk
FOREIGN KEY (usuario_id)
REFERENCES public.usuario (usuario_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.veiculo ADD CONSTRAINT usuario_veiculo_fk
FOREIGN KEY (usuario_id)
REFERENCES public.usuario (usuario_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.avaliacao ADD CONSTRAINT usuario_avaliacao_fk
FOREIGN KEY (usuario_id_avaliador)
REFERENCES public.usuario (usuario_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.avaliacao ADD CONSTRAINT usuario_avaliacao_fk1
FOREIGN KEY (usuario_id_avaliado)
REFERENCES public.usuario (usuario_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.carona ADD CONSTRAINT usuario_carona_fk
FOREIGN KEY (usuario_id)
REFERENCES public.usuario (usuario_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.carona ADD CONSTRAINT endereco_carona_fk
FOREIGN KEY (endereco_id_inicio)
REFERENCES public.endereco (endereco_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.carona ADD CONSTRAINT endereco_carona_fk1
FOREIGN KEY (endereco_id_fim)
REFERENCES public.endereco (endereco_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;

ALTER TABLE public.avaliacao ADD CONSTRAINT carona_avaliacao_fk
FOREIGN KEY (carona_id)
REFERENCES public.carona (carona_id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
NOT DEFERRABLE;