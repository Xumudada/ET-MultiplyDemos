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
    unsafe class ETModel_LandlordsGamerPanelComponent_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ETModel.LandlordsGamerPanelComponent);
            args = new Type[]{};
            method = type.GetMethod("SetReady", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetReady_0);
            args = new Type[]{};
            method = type.GetMethod("GameStart", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GameStart_1);
            args = new Type[]{typeof(System.Boolean)};
            method = type.GetMethod("SetGrab", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetGrab_2);
            args = new Type[]{typeof(ETModel.Identity)};
            method = type.GetMethod("SetIdentity", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetIdentity_3);
            args = new Type[]{};
            method = type.GetMethod("ResetPrompt", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ResetPrompt_4);
            args = new Type[]{};
            method = type.GetMethod("SetDiscard", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetDiscard_5);
            args = new Type[]{};
            method = type.GetMethod("UpdatePanel", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, UpdatePanel_6);
            args = new Type[]{};
            method = type.GetMethod("get_NickName", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_NickName_7);
            args = new Type[]{};
            method = type.GetMethod("SetPlayCardsError", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetPlayCardsError_8);
            args = new Type[]{typeof(UnityEngine.GameObject)};
            method = type.GetMethod("SetPanel", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetPanel_9);

            field = type.GetField("Panel", flag);
            app.RegisterCLRFieldGetter(field, get_Panel_0);
            app.RegisterCLRFieldSetter(field, set_Panel_0);


        }


        static StackObject* SetReady_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetReady();

            return __ret;
        }

        static StackObject* GameStart_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.GameStart();

            return __ret;
        }

        static StackObject* SetGrab_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @isGrab = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetGrab(@isGrab);

            return __ret;
        }

        static StackObject* SetIdentity_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ETModel.Identity @identity = (ETModel.Identity)typeof(ETModel.Identity).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetIdentity(@identity);

            return __ret;
        }

        static StackObject* ResetPrompt_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ResetPrompt();

            return __ret;
        }

        static StackObject* SetDiscard_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetDiscard();

            return __ret;
        }

        static StackObject* UpdatePanel_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.UpdatePanel();

            return __ret;
        }

        static StackObject* get_NickName_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.NickName;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* SetPlayCardsError_8(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetPlayCardsError();

            return __ret;
        }

        static StackObject* SetPanel_9(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.GameObject @panel = (UnityEngine.GameObject)typeof(UnityEngine.GameObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            ETModel.LandlordsGamerPanelComponent instance_of_this_method = (ETModel.LandlordsGamerPanelComponent)typeof(ETModel.LandlordsGamerPanelComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetPanel(@panel);

            return __ret;
        }


        static object get_Panel_0(ref object o)
        {
            return ((ETModel.LandlordsGamerPanelComponent)o).Panel;
        }
        static void set_Panel_0(ref object o, object v)
        {
            ((ETModel.LandlordsGamerPanelComponent)o).Panel = (UnityEngine.GameObject)v;
        }


    }
}
