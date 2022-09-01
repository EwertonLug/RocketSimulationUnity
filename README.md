# RocketSimulationUnity


Projeto feito na Unity 2020.3.30f1

A implementação esta na Pasta Assets/_Project

## Entry point

Tudo sendo feito na cena `RocketLauncherSimulation`

Procurar o objeto `Laucher`, esse é o objeto que contem o script que incia o `Rocket`.

As configurações do Rocket ficam em um `ScriptableObject` na pasta *Assets/_Project/Contents/Rocket * chamdo `RocketData`.

## Architecture
O projeto todo foi feito pensando em respeitar o príncipio de inversão de controle. 

Então o acesso é feito através da interface ou class abstrata.

Foi criado um `ServiceLocator` para disponibilizar de forma desacoplada serviços comuns como Audio e Efeitos.



