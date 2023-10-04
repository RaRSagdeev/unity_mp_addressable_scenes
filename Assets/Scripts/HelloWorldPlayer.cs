using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        private bool ServerSideSceneValidation(int sceneIndex, string sceneName, LoadSceneMode loadSceneMode)
        {
            return false;
        }


        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                NetworkManager.SceneManager.VerifySceneBeforeLoading = ServerSideSceneValidation;
                //NetworkManager.SceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
                //var status = NetworkManager.SceneManager.LoadScene(m_SceneName, LoadSceneMode.Additive);
                //CheckStatus(status);
            }

            base.OnNetworkSpawn();
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public override void OnGainedOwnership()
        {
            Debug.Log($"C scene is {gameObject.scene.name}");
            Debug.Log($"ownership: {IsServer}, {IsOwner}");
            
        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                Position.Value = randomPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        void Update()
        {
            transform.position = Position.Value;
        }
    }
}