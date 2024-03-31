using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordWorkshopTests
{
    private PasswordCanvasWorkshopScript passwordWorkshop;

    [SetUp]
    public void Setup()
    {
        passwordWorkshop = new GameObject().AddComponent<PasswordCanvasWorkshopScript>();
    }

    [Test]
    public void CheckRuleCompliance_LengthRuleCompliance()
    {
        bool isCompliant;
        string passwordTrue;
        string passwordFalse;
        Rule rule = new Rule("Length: Must have at least 8 characters");

        passwordTrue = "12345678";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordTrue = "123456789";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordFalse = "12345";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordFalse);
        Assert.IsFalse(isCompliant);

    }

    [Test]
    public void CheckRuleCompliance_UppercaseRuleCompliance()
    {
        bool isCompliant;
        string passwordTrue;
        string passwordFalse;
        Rule rule = new Rule("Uppercase: Must contain at least one uppercase letter");

        passwordTrue = "Password";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordTrue = "PassWord";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordFalse = "password";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordFalse);
        Assert.IsFalse(isCompliant);
    }

    [Test]
    public void CheckRuleCompliance_LowercaseRuleCompliance()
    {
        bool isCompliant;
        string passwordTrue;
        string passwordFalse;
        Rule rule = new Rule("Lowercase: Must contain at least one lowercase letter");

        passwordTrue = "password";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordTrue = "PASSWORd";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordFalse = "PASSWORD";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordFalse);
        Assert.IsFalse(isCompliant);
    }

    [Test]
    public void CheckRuleCompliance_NumberRuleCompliance()
    {
        bool isCompliant;
        string passwordTrue;
        string passwordFalse;
        Rule rule = new Rule("Number: Must contain at least one number");

        passwordTrue = "password1";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordTrue = "password123";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordFalse = "password";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordFalse);
        Assert.IsFalse(isCompliant);
    }

    [Test]
    public void CheckRuleCompliance_SpecialCharacterRuleCompliance()
    {
        bool isCompliant;
        string passwordTrue;
        string passwordFalse;
        Rule rule = new Rule("Special Character: Must contain at least one special character");

        passwordTrue = "password!";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordTrue = "p@ssword!";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordTrue);
        Assert.IsTrue(isCompliant);

        passwordFalse = "password";
        isCompliant = passwordWorkshop.CheckRuleCompliance(rule, passwordFalse);
        Assert.IsFalse(isCompliant);
    }

}
