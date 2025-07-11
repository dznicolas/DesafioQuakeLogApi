# Quake log parser

## Task 1 - Construa um parser para o arquivo de log games.log e exponha uma API de consulta.

O arquivo games.log é gerado pelo servidor de quake 3 arena. Ele registra todas as informações dos jogos, quando um jogo começa, quando termina, quem matou quem, quem morreu pq caiu no vazio, quem morreu machucado, entre outros.

O parser deve ser capaz de ler o arquivo, agrupar os dados de cada jogo, e em cada jogo deve coletar as informações de morte.

### Exemplo

      21:42 Kill: 1022 2 22: <world> killed Isgalamido by MOD_TRIGGER_HURT
  
  O player "Isgalamido" morreu pois estava ferido e caiu de uma altura que o matou.

      2:22 Kill: 3 2 10: Isgalamido killed Dono da Bola by MOD_RAILGUN
  
  O player "Isgalamido" matou o player Dono da Bola usando a arma Railgun.
  
Para cada jogo o parser deve gerar algo como:

    game_1: {
        total_kills: 45;
        players: ["Dono da bola", "Isgalamido", "Zeh"]
        kills: {
          "Dono da bola": 5,
          "Isgalamido": 18,
          "Zeh": 20
        }
      }


### Observações

1. Quando o `<world>` mata o player ele perde -1 kill.
2. `<world>` não é um player e não deve aparecer na lista de players e nem no dicionário de kills.
3. `total_kills` são os kills dos games, isso inclui mortes do `<world>`.

## Task 2 - Após construir o parser construa uma API que faça a exposição de um método de consulta que retorne um relatório de cada jogo.

## Technologies

- **Language:** C# (.NET 8)  
- **Testing:** xUnit for unit and integration tests (project: `DesafioQuakeLogTest`)  
- **API:** RESTful with JSON responses  
- **Version Control:** Git  

## Project Structure

- `DesafioQuakeLog/` — API project including parser and controllers  
- `DesafioQuakeLogTest/` — Test project using xUnit covering core functionality and API endpoints  

## Setup

1. Clone the repository:
2. Navigate to the API project folder:
3. Build the project:
4. Run the API:

## Solution Explanation

The parser processes the `games.log` file line by line, identifying the start and end of each game, and recording kills and player actions.




