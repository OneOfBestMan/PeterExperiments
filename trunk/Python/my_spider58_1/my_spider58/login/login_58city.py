import time
import requests
from selenium import webdriver

HEADERS = {
        'User-Agent': 'Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) '
                      'Chrome/68.0.3440.106 Mobile Safari/537.36'
    }
LOGIN_URL = 'http://m.m.58.com/login'
LOGIN_INDEX='http://m.m.58.com/index'
# 构造 cookie 信息
session = requests.session()
#session.cookies = cookielib.LWPCookieJar(filename='cookies')

def sele2req_cookie(cookies):
    cookie_dict = dict()
    for cookie in cookies:
        cookie_dict[cookie['name']] = cookie['value']
    return cookie_dict

def isLogin():
    login_code = session.get(LOGIN_INDEX, headers=HEADERS, allow_redirects=False).status_code  #allow_redirects 不允许重定向
    if login_code == 200:
        return False
    else:
        return False

def login(url):
    driver = webdriver.Chrome()
    if isLogin():
          driver.get(LOGIN_INDEX)
          cookies = driver.get_cookies()
          return cookies

    login_name = '13501884996'
    login_password = '58city22'
    
    driver.get(url)
    time.sleep(1)

    username = driver.find_element_by_id('userLoginName')
    username.clear()
    password = driver.find_element_by_id('userLoginPwd')
    password.clear()
    username.send_keys(login_name)
    password.send_keys(login_password)

    submit = driver.find_element_by_id('userLoginBtn')
    time.sleep(1)
    submit.click()
    time.sleep(3)

    cookies = driver.get_cookies()
    #driver.close()
    return cookies


if __name__ == '__main__':
    # 这个地方是通过观察html代码得到的，因为我先前通过find方法定位switch始终提示我没有这个元素，那么我就猜想它肯定是被隐藏或者嵌套在别的
    # frame中了
  
    cookies = login(LOGIN_URL)

    # 验证cookie是否正确
    req_cookies = sele2req_cookie(cookies)
    content = requests.get(LOGIN_INDEX, headers=headers, cookies=req_cookies)
    print(content.text)