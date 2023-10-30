using System;
using System.Diagnostics;

Random rand = new();

int Attackers = 1000;
int Defenders = 585; //585

int rounds = 10000;

int AttackersWins = 0;
int DefendersWins = 0;


Stopwatch watch = new();
watch.Start();

Parallel.For(0, rounds, i => {
    lock(rand)
    {
        while(Attackers > 1 && Defenders > 0)
    {
        Battle();
    }
    
    if(Attackers < 2)
    {
        DefendersWins++;
    }

    if(Defenders < 1)
    {
        AttackersWins++;
    }

    Attackers = 1000;
    Defenders = 585;
    }
});


// for(int i = 0; i < rounds; i++)
// {
//     while(Attackers > 1 && Defenders > 0)
//     {
//         Battle();
//     }
    
//     if(Attackers < 2)
//     {
//         DefendersWins++;
//     }

//     if(Defenders < 1)
//     {
//         AttackersWins++;
//     }

//     Attackers = 1000;
//     Defenders = 585;
// }

watch.Stop();
Console.WriteLine("Tempo: " + (float) watch.ElapsedMilliseconds /1000);

Console.WriteLine("Ataque: " + ((float) AttackersWins / rounds * 100) + "%");
Console.WriteLine("Defesa: " + ((float) DefendersWins / rounds * 100) + "%");

int Roll()
    => rand.Next(6) + 1;

int[] SortedDiceValues(int qtd)
{   
    int[] diceValues = {0, 0, 0};

    for(int i = 0; i < qtd; i++)
    {
        diceValues[i] = Roll();
    }

    Array.Sort(diceValues);
    Array.Reverse(diceValues);
    return diceValues;
}

void Battle()
{
    int AttackersSquad = 3;
    int DefendersSquad = 3;

    int turns = 3;

    if(Attackers < 4)
    {
        AttackersSquad = Attackers - 1;
        turns = AttackersSquad;
    }

    if (Defenders < 4)
    {
        DefendersSquad = Defenders;

        if(turns < DefendersSquad)
            turns = DefendersSquad;
    }

    int[] AttackersValues = SortedDiceValues(AttackersSquad); 
    int[] DefendersValues = SortedDiceValues(AttackersSquad); 

    for(int i = 0; i < turns; i++)
    {
        if(AttackersValues[i] > DefendersValues[i])
            DefendersSquad--;
        else
            AttackersSquad--;
    }

    Attackers -= 3;
    Defenders -= 3;

    Attackers += AttackersSquad;
    Defenders += DefendersSquad;

}
