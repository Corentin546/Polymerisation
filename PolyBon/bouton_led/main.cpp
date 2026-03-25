#include <iostream>
#include <fstream>
#include <string>
#include <unistd.h>

int main() {
    std::cout << "--- Demarrage du controle GPIO ---" << std::endl;

    // 1. Preparation des pins (Export)
    // On utilise le 23 pour le bouton et le 24 pour la LED
    std::ofstream exp("/sys/class/gpio/export");
    exp << "23";
    exp.flush();
    exp << "24";
    exp.close();

    // Pause pour laisser Linux creer les dossiers
    usleep(200000);

    // 2. Configuration des directions
    std::ofstream("/sys/class/gpio/gpio23/direction") << "in";
    std::ofstream("/sys/class/gpio/gpio24/direction") << "out";

    std::cout << "Systeme pret ! (Appuyez sur le bouton)" << std::endl;

    std::string etatBouton;

    while (true) {
        // LIRE le bouton (GPIO 23)
        std::ifstream bouton("/sys/class/gpio/gpio23/value");
        bouton >> etatBouton;
        bouton.close();

        // ECRIRE la valeur sur la LED (GPIO 24)
        std::ofstream led("/sys/class/gpio/gpio24/value");
        led << etatBouton;
        led.close();

        // Petit affichage pour confirmer dans la console
        std::cout << "\rBouton : " << etatBouton << " -> LED : " << etatBouton << std::flush;

        usleep(20000); // 20ms de delai pour ne pas surcharger le processeur
    }

    return 0;
}