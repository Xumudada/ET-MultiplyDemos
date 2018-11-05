using System;
using ETModel;

namespace ETHotfix
{
	public class OuterMessageDispatcher: IMessageDispatcher
	{
		public void Dispatch(Session session, ushort opcode, object message)
		{
			DispatchAsync(session, opcode, message).NoAwait();
		}
		
		public async ETVoid DispatchAsync(Session session, ushort opcode, object message)
		{
			try
			{
				switch (message)
				{
                    case IActorLocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                        {
                            long actorId = session.GetComponent<SessionUserComponent>().User.ActorIDforClient;
                            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(actorId);

                            int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                            IResponse response = await actorLocationSender.Call(actorLocationRequest);
                            response.RpcId = rpcId;

                            session.Reply(response);
                            return;
                        }
                    case IActorLocationMessage actorLocationMessage:
                        {
                            long actorId = session.GetComponent<SessionUserComponent>().User.ActorIDforClient;
                            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(actorId);
                            actorLocationSender.Send(actorLocationMessage);
                            return;
                        }
                    case IActorRequest iActorRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                        {
                            long actorId = session.GetComponent<SessionUserComponent>().User.ActorIDforClient;
                            ActorMessageSender actorMessageSender = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(actorId);

                            int rpcId = iActorRequest.RpcId; // 这里要保存客户端的rpcId
                            IResponse response = await actorMessageSender.Call(iActorRequest);
                            response.RpcId = rpcId;

                            session.Reply(response);
                            return;
                        }
                    case IActorMessage iActorMessage: // gate session收到actor消息直接转发给actor自己去处理
                        {
                            long actorId = session.GetComponent<SessionUserComponent>().User.ActorIDforClient;
                            ActorMessageSender actorMessageSender = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(actorId);
                            actorMessageSender.Send(iActorMessage);
                            return;
                        }
                }

				Game.Scene.GetComponent<MessageDispatcherComponent>().Handle(session, new MessageInfo(opcode, message));
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}
	}
}
