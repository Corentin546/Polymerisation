#include <iostream>
#include <pigpio.h>

int main() {
    // Initialisation du matériel
    if (gpioInitialise() < 0) {
        std::cout << "Erreur d'initialisation. Tapez 'sudo pigpiod' sur la Pi." << std::endl;
        return 1;
    }

    const int LED_PIN = 24; // Broche physique 12
    gpioSetMode(LED_PIN, PI_OUTPUT);

    // ALLUMAGE
    std::cout << "LED allumee..." << std::endl;
    gpioWrite(LED_PIN, 1);

    // ATTENTE de 10 secondes (en microsecondes)
    gpioDelay(10000000);

    // EXTINCTION
    gpioWrite(LED_PIN, 0);
    std::cout << "LED eteinte." << std::endl;

    gpioTerminate();
    return 0;
}