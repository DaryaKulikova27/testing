package ru.shop.quatle.operations;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class Authorization {
    private WebDriver driver;
    private final String setAuthorizationName = "login";
    private final String setPasswordName = "password";
    private final String buttonXpath = "//*[@id=\"login\"]/button";
    private final String goToMainPageButtonXpath = "//a[text()='Главная']";

    public Authorization(WebDriver driver){
        this.driver = driver;
    }

    public Authorization setLogin(String login) {
        WebElement loginInput = driver.findElement(By.name(setAuthorizationName));
        loginInput.sendKeys(login);
        return this;
    }

    public Authorization setPassword(String password) {
        WebElement passwordInput = driver.findElement(By.name(setPasswordName));
        passwordInput.sendKeys(password);
        return this;
    }

    public Authorization clickEnter(){
        WebElement loginButton = driver.findElement(By.xpath(buttonXpath));
        loginButton.click();
        return this;
    }

    public Authorization goToMainPage(){
        WebElement loginButton = driver.findElement(By.xpath(goToMainPageButtonXpath));
        loginButton.click();
        return this;
    }
}
