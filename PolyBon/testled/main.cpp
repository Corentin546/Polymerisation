#include <iostream>
#include <vector>
#include <pigpio.h>
#include <unistd.h> // Pour sleep()

// On définit les numéros des pins GPIO (BCM)
const std::vector<int> LED_PINS = { 26, 19, 13, 6 };

int main() {
    // 1. Initialisation de la bibliothèque pigpio
    if (gpioInitialise() < 0) {
        std::cerr << "Erreur : Impossible d'initialiser pigpio" << std::endl;
        return 1;
    }

    // 2. Configuration des pins en mode SORTIE (Output)
    for (int pin : LED_PINS) {
        gpioSetMode(pin, PI_OUTPUT);
    }

    std::cout << "Allumage des 4 LEDs..." << std::endl;

    // 3. Allumer toutes les LEDs
    for (int pin : LED_PINS) {
        gpioWrite(pin, 1);
    }

    // 4. Attendre 10 secondes
    sleep(10);

    // 5. Éteindre toutes les LEDs
    for (int pin : LED_PINS) {
        gpioWrite(pin, 0);
    }

    std::cout << "Les LEDs sont éteintes. Fin du programme." << std::endl;

    // 6. Libérer les ressources
    gpioTerminate();

    return 0;
}