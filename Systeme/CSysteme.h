///////////////////////////////////////////////////////////
//  CSysteme.h
//  Implementation of the Class CSysteme
//  Created on:      05-mars-2026 09:51:42
//  Original author: The Administrator
///////////////////////////////////////////////////////////

#if !defined(EA_16B07617_4486_42f7_B49A_0FEA3BF204B4__INCLUDED_)
#define EA_16B07617_4486_42f7_B49A_0FEA3BF204B4__INCLUDED_

#include "CGest_Capt.h"
#include "CGest_Sorties.h"
#include "CServeur_Web.h"
#include "CBase_de_données.h"

class CSysteme
{

public:
	CSysteme();
	virtual ~CSysteme();
	CGest_Capt *m_CGest_Capt;
	CGest_Sorties *m_CGest_Sorties;
	CServeur_Web *m_CServeur_Web;
	CBase_de_données *m_CBase_de_données;

	void Choisir_Durée();
	void recevoir();

};
#endif // !defined(EA_16B07617_4486_42f7_B49A_0FEA3BF204B4__INCLUDED_)
