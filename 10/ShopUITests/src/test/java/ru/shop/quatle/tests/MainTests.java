package ru.shop.quatle.tests;

import org.junit.Assert;
import org.junit.Before;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;
import org.openqa.selenium.chrome.ChromeDriver;

public class MainTests {
    public ChromeDriver driver;

    @BeforeMethod
    public void setUp() {
        System.setProperty("webdriver.chrome.driver", "C:\\Users\\User\\Documents\\chromedriver");
        driver = new ChromeDriver();
        System.out.println("Test start");
    }

    @Test
    public void exampleTest() {
        driver.get("http://shop.qatl.ru/");
        String title = driver.getTitle();
        Assert.assertTrue(title.equals("Главная страница"));
    }
}
