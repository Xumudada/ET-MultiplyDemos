using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class ETModel_GamerPathComponent_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ETModel.GamerPathComponent);

            field = type.GetField("CancellationTokenSource", flag);
            app.RegisterCLRFieldGetter(field, get_CancellationTokenSource_0);
            app.RegisterCLRFieldSetter(field, set_CancellationTokenSource_0);
            field = type.GetField("Path", flag);
            app.RegisterCLRFieldGetter(field, get_Path_1);
            app.RegisterCLRFieldSetter(field, set_Path_1);
            field = type.GetField("ServerPos", flag);
            app.RegisterCLRFieldGetter(field, get_ServerPos_2);
            app.RegisterCLRFieldSetter(field, set_ServerPos_2);


        }



        static object get_CancellationTokenSource_0(ref object o)
        {
            return ((ETModel.GamerPathComponent)o).CancellationTokenSource;
        }
        static void set_CancellationTokenSource_0(ref object o, object v)
        {
            ((ETModel.GamerPathComponent)o).CancellationTokenSource = (System.Threading.CancellationTokenSource)v;
        }
        static object get_Path_1(ref object o)
        {
            return ((ETModel.GamerPathComponent)o).Path;
        }
        static void set_Path_1(ref object o, object v)
        {
            ((ETModel.GamerPathComponent)o).Path = (System.Collections.Generic.List<UnityEngine.Vector3>)v;
        }
        static object get_ServerPos_2(ref object o)
        {
            return ((ETModel.GamerPathComponent)o).ServerPos;
        }
        static void set_ServerPos_2(ref object o, object v)
        {
            ((ETModel.GamerPathComponent)o).ServerPos = (UnityEngine.Vector3)v;
        }


    }
}
