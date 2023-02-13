//参考: http://wiki.unity3d.com/index.php?title=Singleton
using UnityEngine;
 
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;
 
    public static T Instance {
        get {
            //Scene内にあったら取得
            _instance = _instance ? _instance : (FindObjectOfType (typeof(T)) as T);
            //TをアタッチしたGameObject生成してT取得
            _instance = _instance ? _instance : new GameObject (typeof(T).ToString (), typeof(T)).GetComponent<T> ();
            return _instance;
        }
    }
 
    private void OnApplicationQuit ()
    {
        _instance = null;
    }
}