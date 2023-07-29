using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class Mail {}
public class GameOverMail : Mail { public bool isWon; }
public class VFXRequestMail : Mail { public GameObject spawner; public AssetReferenceT<GameObject> vfxAssetReference; }
