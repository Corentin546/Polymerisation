///////////////////////////////////////////////////////////
//  CBase_de_données.h
//  Implementation of the Class CBase_de_données
//  Created on:      05-mars-2026 09:51:15
//  Original author: The Administrator
///////////////////////////////////////////////////////////

#if !defined(EA_245FD5C3_3EF1_44d4_A1AD_09D8E13ECDA4__INCLUDED_)
#define EA_245FD5C3_3EF1_44d4_A1AD_09D8E13ECDA4__INCLUDED_

class CBase_de_données
{

public:
	CBase_de_données();
	virtual ~CBase_de_données();

	void connecter();
	void déconnecter();
	void écrire();
	void lire();
	void Rechercher_Données();

};
#endif // !defined(EA_245FD5C3_3EF1_44d4_A1AD_09D8E13ECDA4__INCLUDED_)
