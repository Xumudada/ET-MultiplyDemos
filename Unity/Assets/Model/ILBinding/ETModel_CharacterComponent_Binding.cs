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
    unsafe class ETModel_CharacterComponent_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ETModel.CharacterComponent);

            field = type.GetField("mobaID", flag);
            app.RegisterCLRFieldGetter(field, get_mobaID_0);
            app.RegisterCLRFieldSetter(field, set_mobaID_0);
            field = type.GetField("group", flag);
            app.RegisterCLRFieldGetter(field, get_group_1);
            app.RegisterCLRFieldSetter(field, set_group_1);
            field = type.GetField("type", flag);
            app.RegisterCLRFieldGetter(field, get_type_2);
            app.RegisterCLRFieldSetter(field, set_type_2);
            field = type.GetField("route", flag);
            app.RegisterCLRFieldGetter(field, get_route_3);
            app.RegisterCLRFieldSetter(field, set_route_3);
            field = type.GetField("anime", flag);
            app.RegisterCLRFieldGetter(field, get_anime_4);
            app.RegisterCLRFieldSetter(field, set_anime_4);
            field = type.GetField("isAlive", flag);
            app.RegisterCLRFieldGetter(field, get_isAlive_5);
            app.RegisterCLRFieldSetter(field, set_isAlive_5);
            field = type.GetField("isInvisible", flag);
            app.RegisterCLRFieldGetter(field, get_isInvisible_6);
            app.RegisterCLRFieldSetter(field, set_isInvisible_6);
            field = type.GetField("isAttackable", flag);
            app.RegisterCLRFieldGetter(field, get_isAttackable_7);
            app.RegisterCLRFieldSetter(field, set_isAttackable_7);
            field = type.GetField("attackRange", flag);
            app.RegisterCLRFieldGetter(field, get_attackRange_8);
            app.RegisterCLRFieldSetter(field, set_attackRange_8);


        }



        static object get_mobaID_0(ref object o)
        {
            return ((ETModel.CharacterComponent)o).mobaID;
        }
        static void set_mobaID_0(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).mobaID = (System.Int32)v;
        }
        static object get_group_1(ref object o)
        {
            return ((ETModel.CharacterComponent)o).group;
        }
        static void set_group_1(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).group = (System.Int32)v;
        }
        static object get_type_2(ref object o)
        {
            return ((ETModel.CharacterComponent)o).type;
        }
        static void set_type_2(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).type = (System.Int32)v;
        }
        static object get_route_3(ref object o)
        {
            return ((ETModel.CharacterComponent)o).route;
        }
        static void set_route_3(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).route = (System.Int32)v;
        }
        static object get_anime_4(ref object o)
        {
            return ((ETModel.CharacterComponent)o).anime;
        }
        static void set_anime_4(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).anime = (ETModel.GamerAnimatorComponent)v;
        }
        static object get_isAlive_5(ref object o)
        {
            return ((ETModel.CharacterComponent)o).isAlive;
        }
        static void set_isAlive_5(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).isAlive = (System.Boolean)v;
        }
        static object get_isInvisible_6(ref object o)
        {
            return ((ETModel.CharacterComponent)o).isInvisible;
        }
        static void set_isInvisible_6(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).isInvisible = (System.Boolean)v;
        }
        static object get_isAttackable_7(ref object o)
        {
            return ((ETModel.CharacterComponent)o).isAttackable;
        }
        static void set_isAttackable_7(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).isAttackable = (System.Boolean)v;
        }
        static object get_attackRange_8(ref object o)
        {
            return ((ETModel.CharacterComponent)o).attackRange;
        }
        static void set_attackRange_8(ref object o, object v)
        {
            ((ETModel.CharacterComponent)o).attackRange = (System.Single)v;
        }


    }
}
