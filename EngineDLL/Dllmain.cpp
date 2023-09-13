// Dllmain.cpp : Définit le point d'entrée de l'application DLL.

#pragma comment(lib, "Engine.lib")

#define WIN32_LEAN_AND_MEAN             // Exclure les en-têtes Windows rarement utilisés
#include <Windows.h>
#include <crtdbg.h>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
#if _DEBUG
		// Enable run-time memory check for debug builds.
		_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
#endif // _DEBUG
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

