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
          username.send_keys(name)
          password.send_keys(passwd)
          
          submit = driver.find_element_by_css_selector('button[type=\"button\"]')
          time.sleep(1)
          submit.click()
          time.sleep(2)
          cookie_dict=self._sele2req_cookie(driver.get_cookies())
          return cookie_dict

      def _sele2req_cookie(self,cookies):
          cookie_dict = dict()
          for cookie in cookies:
              cookie_dict[cookie['name']] = cookie['value']
          return cookie_dict


