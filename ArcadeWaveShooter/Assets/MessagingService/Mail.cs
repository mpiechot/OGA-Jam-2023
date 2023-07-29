using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class Mail {}
public class GameOverMail : Mail { public bool isWon; }
public class VFXRequestMail : Mail { public GameObject spawner; public Vector3 spawnPosition; public AssetReferenceT<GameObject> vfxAssetReference; }
public class HeatLimitReachedMail : Mail { }
public class HeatDecreaseMail : Mail { public float decreaseAmount; }