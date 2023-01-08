using DevLab.JmesPath.Interop;
using jmespath_intro.Functions;
using System.Reflection;

namespace DevLab.JmesPath.Functions;

public sealed class Registrations
{
    public static IRegisterFunctions Register(IRegisterFunctions repository)
    {
        repository
            .Register<NowFunction>();

        return repository;
    }

    public static void LoadJmesPathFunctionRegistrations()
    {

    }
}