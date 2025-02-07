﻿#region

using GameJam.Core.States;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

#endregion

[TestFixture]
public class DeathTests : GGJTests
{
#region Setup/Teardown Methods

    [SetUp]
    public void Setup()
    {
        foreach (var gameObject in Object.FindObjectsOfType<GameObject>())
            Object.DestroyImmediate(gameObject);
    }

#endregion

#region Test Methods

    [Test(Description = "進入死亡，消失")]
    public void _01_Disappear_On_Enter()
    {
        var monsterA = Give_A_Monster1();
        var targetB  = Give_A_Monster1();
        var death    = new Death(monsterA);
        death.OnEnter();

        var monster1s = Object.FindObjectsOfType<Monster1>();
        Assert.AreEqual(1 , monster1s.Length);
        Assert.AreEqual(targetB , monster1s[0]);
    }

#endregion

#region Public Methods

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        EditorSceneManager.OpenScene("Assets/GameJam/TestScene.unity" , OpenSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        EditorSceneManager.OpenScene("Assets/GameJam/GameJamScene.unity" , OpenSceneMode.Single);
    }

#endregion
}