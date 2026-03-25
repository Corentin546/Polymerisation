#include <iostream>
#include <vector>
#include <pigpio.h>

// On liste toutes les LEDs dans un tableau (vector) pour plus de simplicité
const std::vector<int> LED_PINS = { 26, 19, 13, 6 ,5};
const int BUTTON_PIN = 25;

int main() {
    // 1. Initialisation
    if (gpioInitialise() < 0) {
        std::cerr << "Erreur d'initialisation pigpio" << std::endl;
        return 1;
    }

    // 2. Configuration des broches
    // On configure chaque LED du tableau en mode SORTIE
    for (int pin : LED_PINS) {
        gpioSetMode(pin, PI_OUTPUT);
    }

    gpioSetMode(BUTTON_PIN, PI_INPUT);
    gpioSetPullUpDown(BUTTON_PIN, PI_PUD_UP);

    std::cout << "Prêt ! Appuie sur le bouton pour allumer les 4 LEDs pendant 2s." << std::endl;

    while (true) {
        // 3. Détection de l'appui (0 = pressé)
        if (gpioRead(BUTTON_PIN) == 0) {
            std::cout << "Bouton détecté ! Allumage des LEDs..." << std::endl;

            // Allumer toutes les LEDs
            for (int pin : LED_PINS) {
                gpioWrite(pin, 1);
            }

            gpioDelay(2000000); // Attendre 2 secondes

            // Éteindre toutes les LEDs
            for (int pin : LED_PINS) {
                gpioWrite(pin, 0);
            }

            std::cout << "LEDs éteintes." << std::endl;

            // Anti-rebond et pause pour éviter les déclenchements multiples
            gpioDelay(300000);
        }

        gpioDelay(10000); // 10ms de repos CPU
    }

    gpioTerminate();
    return 0;
}