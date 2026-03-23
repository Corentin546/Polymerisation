#include <iostream>
#include "CServeur_Web.h"

int main() {

    CServeur_Web monServeur;

    monServeur.AnalyserRequete();

    monServeur.Demande_Action();

    monServeur.Demande_IHM();

    return 0; 
}