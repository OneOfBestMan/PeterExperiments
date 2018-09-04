#获取Cockie，登陆
import requests
from selenium import webdriver
import time
class CockieProvider():
      def __init__(self,taskOption):
          self.taskOption=taskOption

      def login(self,name, passwd):
          driver = webdriver.Chrome()
          driver.get(self.taskOption.baseUrl)
          time.sleep(1)
          login_button = driver.find_element_by_id('login_b')
          login_button.click()
          time.sleep(1)
          
          driver.switch_to_frame("layui-layer-iframe1")
          time.sleep(1)
          
          username = driver.find_element_by_id('inputEmail')
          username.clear()
          password = driver.find_element_by_id('inputPassword')
          password.clear()
          username.send_keys(login_name)
          password.send_keys(login_password)
          
          submit = driver.find_element_by_css_selector('button[type=\"button\"]')
          time.sleep(1)
          submit.click()
          time.sleep(2)
          return driver.get_cookies()


