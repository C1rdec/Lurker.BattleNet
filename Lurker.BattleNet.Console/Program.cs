// See https://aka.ms/new-console-template for more information
using Lurker.BattleNet.Services;

Console.WriteLine("Hello, World!");

var service = new BattleNetService();
service.FindGames();