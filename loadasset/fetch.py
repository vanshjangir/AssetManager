from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class Assets:
    def __init__(self):
        C_options = webdriver.ChromeOptions()
        C_options.add_argument("headless")
        C_options.add_argument('--disable-popup-blocking')
        C_options.add_experimental_option('prefs', {
            'download.default_directory': '/home/vansh/GodotGames/FirstGame/DownloadedAssets/'
        })
        try:
            self.driver = webdriver.Chrome(options=C_options)
        except Exception as e:
            print("Exception occured ", e)

    def load(self,scroll):
        self.driver.get('https://itch.io/game-assets/free')
        AssetList = []
        try:
            count = 0
            parent_element = self.driver.find_elements(By.CLASS_NAME, "game_cell.has_cover")
            for each in parent_element:
                count += 1
                asset_data = {"image":each.find_element(By.CLASS_NAME,"game_thumb").find_element(By.TAG_NAME,"a").find_element(By.TAG_NAME,"img").get_attribute("src"), 
                              "text":each.find_element(By.CLASS_NAME, "game_cell_data").find_element(By.TAG_NAME, "a").text, 
                              "link":each.find_element(By.CLASS_NAME, "game_cell_data").find_element(By.TAG_NAME, "a").get_attribute("href")}
                AssetList.append(asset_data)
                self.driver.execute_script("window.scrollTo(0, 500)")
                
                if count >= 8:
                    break
            self.driver.execute_script(f"window.scrollTo(0, {scroll})")
            return AssetList
        except Exception as e:
            return e
    
    def download(self,url):
        self.driver.get(url)
        try:
            first_download_button = self.driver.find_element(By.CLASS_NAME, "button.buy_btn")
            first_download_button.click()

            wait = WebDriverWait(self.driver, 10)
            direct_download_button = wait.until(EC.presence_of_element_located((By.CLASS_NAME, 'direct_download_btn')))
            direct_download_button.click()
            
            wait = WebDriverWait(self.driver, 10)
            final_download_buttons =  wait.until(EC.presence_of_all_elements_located((By.CLASS_NAME, 'button.download_btn')))

            for each in final_download_buttons:
                each.click()

        except Exception as e:
            print(e)

