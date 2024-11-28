CREATE DATABASE quiz;

use quiz;

SELECT * FROM perguntas;
SELECT * FROM categorias;
SELECT * FROM rankings;

delete from rankings where id != 1;

drop table perguntas;
drop table rankings;
drop table categorias;

DELETE FROM perguntas;

CREATE TABLE categorias(
	id INT auto_increment PRIMARY KEY,
    nome char(255) NOT NULL
);

CREATE TABLE perguntas (
	id int auto_increment PRIMARY KEY,
    enunciado char(255),
	alternativa1 char(255),
    alternativa2 char(255),
    alternativa3 char(255),
    alternativa4 char(255),
    resposta_correta char(255),
    dica char(255),
    fk_categoria_id int
);

  
CREATE TABLE rankings(
	id int auto_increment PRIMARY KEY,
    nome char(255) NOT NULL,
    pontuacao int,
    tempo int
);
  
ALTER TABLE perguntas ADD CONSTRAINT FK_Pergunta_Categoria
	FOREIGN KEY (fk_categoria_id)
	REFERENCES categorias (id)
	ON DELETE CASCADE;
    
insert into categoritempoas (nome) values ("Conhecimentos Gerais");
insert into categorias (nome) values ("Administração");
insert into categorias (nome) values ("Inglês");
insert into categorias (nome) values ("Gastronomia");
insert into categorias (nome) values ("Segurança do Trabalho");

insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Parramos', 'tC', 'Fleetwood', 'Matrix', 'Tacoma', 'Matrix', 'Bisiu', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Jakhaly', 'Nuova 500', 'Escort', 'Compass', 'Express 1500', 'Nuova 500', 'Bisuo', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Zheleznodorozhnyy', 'Silhouette', 'Tacoma', 'Corvette', 'Impala', 'Tacoma', 'Back', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Bechlín', 'Taurus X', 'Nubira', 'Tiburon', 'Ram Van B250', 'Tiburon', 'Bibili', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Mamburao', 'S-Class', '750', 'R8', 'Insight', 'R8', 'Julio', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Xincheng Chengguanzhen', 'Yukon XL 1500', 'LHS', '8 Series', 'Interceptor', '8 Series', 'sadfasf', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Arroyo Seco', 'Ram Van 2500', 'Paseo', 'Q7', 'Rendezvous', 'Rendezvous', 'Dicains', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Mossendjo', 'ES', 'Neon', 'Truck', 'Caravan', 'ES', 'dicats', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Jiaqiao', 'Murciélago', 'CLS-Class', 'Suburban 2500', 'Lancer', 'CLS-Class', 'diducks', 1);
insert into perguntas (enunciado, alternativa1, alternativa2, alternativa3, alternativa4, resposta_correta, dica, fk_categoria_id) values ('Goya', 'Sportage', 'Thunderbird', 'Ram Van B350', 'E-Series', 'Thunderbird', 'diponey', 1);
