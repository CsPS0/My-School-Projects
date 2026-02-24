import sys
import time
import random
import os
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium_stealth import stealth
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.keys import Keys

def human_type(element, text):
    element.clear()
    for char in text:
        element.send_keys(char)
        time.sleep(random.uniform(0.1, 0.2))

def run_stealth_test():
    print("Starting Stealth Mode (Selenium-Stealth)...")
    
    chrome_options = Options()
    chrome_options.binary_location = r"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe"
    chrome_options.add_argument("--start-maximized")
    chrome_options.add_experimental_option("excludeSwitches", ["enable-automation"])
    chrome_options.add_experimental_option('useAutomationExtension', False)
    chrome_options.add_argument("--disable-blink-features=AutomationControlled")

    driver = None
    try:
        driver = webdriver.Chrome(options=chrome_options)
        stealth(driver, languages=["hu-HU", "hu"], vendor="Google Inc.", platform="Win32",
                webgl_vendor="Intel Inc.", renderer="Intel Iris OpenGL Engine", fix_hairline=True)

        wait = WebDriverWait(driver, 40)
        actions = ActionChains(driver)
        
        print("\n[Step 0] Navigating to Allegro...")
        driver.get("https://allegro.hu")
        
        time.sleep(3)
        try:
            cookie_btn = wait.until(EC.element_to_be_clickable((By.XPATH, "//button[contains(., 'Elfogadom') or contains(., 'Hozzájárulok')]")))
            cookie_btn.click()
            print("Cookies handled.")
        except: pass

        time.sleep(3)

        print("\n[Step 3] Testing Search Input...")
        search_input = wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, "input[type='search'], input[name='string']")))
        search_input.click()
        human_type(search_input, "lego")
        time.sleep(2)

        print("\n[Step 4] Submitting Search...")
        search_input.send_keys(Keys.ENTER)
        wait.until(lambda d: "listing" in d.current_url or "string=" in d.current_url or "kereses" in d.current_url)
        print(f"Results loaded.")
        time.sleep(3)

        print("\n[Step 1] Adding product to cart...")
        product_link = wait.until(EC.element_to_be_clickable((By.CSS_SELECTOR, "article h2 a, article h3 a, [data-role='offer-title'] a")))
        product_link.click()
        
        add_btn = wait.until(EC.element_to_be_clickable((By.CSS_SELECTOR, "[data-role='add-to-cart'], button[aria-label*='kosár'], #add-to-cart-button")))
        add_btn.click()
        print("Product added to cart.")
        
        time.sleep(3)
        try:
            continue_btn = driver.find_element(By.XPATH, "//button[contains(., 'FOLYTATÁSA')]")
            continue_btn.click()
            print("Clicked Continue Shopping button.")
        except:
            actions.send_keys(Keys.ESCAPE).perform()
            print("Sent ESC.")

        time.sleep(2)

        print("\n[Step 2] Testing Logo navigation...")
        logo_selectors = [
            "a[aria-label='Allegro']", 
            "a[title='Allegro']", 
            "a[href='/'] img[alt='Allegro']",
            "a[href='/']",
            "header a[data-analytics-click*='Logo']",
            ".logo a"
        ]
        
        logo = None
        for sel in logo_selectors:
            try:
                logo = driver.find_element(By.CSS_SELECTOR, sel)
                if logo.is_displayed():
                    break
            except: continue
            
        if logo:
            logo.click()
            print("Logo clicked.")
        else:
            driver.get("https://allegro.hu")
            
        wait.until(lambda d: "allegro.hu" in d.current_url and d.current_url.count("/") <= 3)
        print("Back to home.")
        time.sleep(3)

        print("\n[Step 5] Testing Login error...")
        print("Cooling down for 10 seconds to avoid block...")
        time.sleep(10)
        
        driver.get("https://allegro.hu/bejelentkezes")
        
        if "blocked" in driver.title.lower() or "Letiltották" in driver.page_source:
            print("!! Blocked by Akamai !! Attempting to solve manually or waiting...")
            time.sleep(15)
        
        login_input = wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, "input[name='login'], #login")))
        human_type(login_input, "error@test.hu")
        pass_input = driver.find_element(By.CSS_SELECTOR, "input[name='password'], #password")
        human_type(pass_input, "wrongpass123")
        driver.find_element(By.CSS_SELECTOR, "button[type='submit']").click()
        
        time.sleep(5)
        print("\n--- ALL STEALTH TESTS COMPLETED SUCCESSFULLY ---")

    except Exception as e:
        print(f"\n[!] Error: {type(e).__name__}: {str(e)}")
        if driver: driver.save_screenshot("error_screenshot.png")
        if sys.stdin.isatty():
            input("Press Enter to close...")
    finally:
        if driver: driver.quit()

if __name__ == "__main__":
    run_stealth_test()
