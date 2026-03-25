#include <iostream>
#include <pigpio.h>
#include <unistd.h> // Pour la fonction sleep()

// On définit le numéro du pin GPIO (schéma Broadcom / BCM)
const int LED_PIN = 19;

int main() {
    // 1. Initialisation de la bibliothèque pigpio
    if (gpioInitialise() < 0) {
        std::cerr << "Erreur : Impossible d'initialiser pigpio" << std::endl;
        return 1;
    }

    // 2. Configuration du pin en mode SORTIE (Output)
    gpioSetMode(LED_PIN, PI_OUTPUT);

    std::cout << "Allumage de la LED sur le pin " << LED_PIN << "..." << std::endl;

    // 3. Allumer la LED (High / 1)
    gpioWrite(LED_PIN, 1);

    // 4. Attendre 10 secondes
    sleep(10);

    // 5. Éteindre la LED (Low / 0)
    gpioWrite(LED_PIN, 0);
    std::cout << "La LED est éteinte. Fin du programme." << std::endl;

    // 6. Libérer les ressources
    gpioTerminate();

    return 0;
}