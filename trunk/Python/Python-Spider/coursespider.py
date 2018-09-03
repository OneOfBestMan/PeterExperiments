
# 引入python自带的http请求库
from urllib import request
import re
import requests
import os
import http.cookiejar
# mock http request 
# 正则\s\S匹配所有字符 *代表匹配0次或无线多次 ?代表非贪婪模式 () 代表组 正则匹配之后只取到需要的数据
class CourseSpider():
    urlbase='https://www.youngmaker.com'
    url = 'https://www.youngmaker.com/home/courses/study/capid/7255/catid/292/sectionid/2100/classroomid/29/ccid/14.html'
    # 匹配所有符合的大的节点
    VIDEOALL = '<div class="mulu" ([\s\S]*?)</div>'
    # 匹配名称
    #VIDEOA = '<a class="study_data" href="(.+)">(.+)</a>'
    VIDEOA = 'href="([^\"]*)"'
    # 所有的目标链接
    COURSEURL =[]
	# 所有的链接
    def __init__(self):
        self.my_url = 'https://www.youngmaker.com/home/courses/study/capid/7255/catid/292/sectionid/2100/classroomid/29/ccid/14.html'
        self.user_agent = 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36'
        self.cookies="yjs_id=ffcbd409b2e680e280b6e8221189d3d3; PHPSESSID=568bcu9pvg6bcgnn2h4doie4kb; CNZZDATA1262355882=264602239-1530607552-https%253A%252F%252Fwww.baidu.com%252F%7C1535929219; ctrl_time=1"
        self.cookie_dir = 'D:/cookie_youngmaker.txt'
        self.user_headers = {
            'Accept': "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
            'Accept - Encoding': "gzip, deflate, br",
            'Connection': "Keep-Alive",
            'User-Agent': self.user_agent,
            'Cookie': self.cookies
        }

    def __fetch_content(self):
        res = request.Request(url=self.my_url,headers=self.user_headers)
        r = request.urlopen(res)
        htmls = r.read()
        htmls = str(htmls, encoding='utf-8')
        return htmls

    def __getcourseurl(self,htmls):
        courseurl=re.findall(CourseSpider.VIDEOALL,htmls)
        urls=re.findall(CourseSpider.VIDEOA,courseurl[0], re.I|re.S|re.M)
        anchors=[]
        for url in urls:
            anchors.append(CourseSpider.urlbase+url)
        return anchors;

    # 数据精炼 去掉空格和换行符
    def __refine(self, anchors):
        l = lambda anchor: {
          'name': anchor['name'][0].strip(),
          'number': anchor['number'][0]
          }
        return map(l, anchors)

    # 打印排序的结果
    def __show(self, anchors):
        for anchor in anchors:
            print(anchor)

    def __down(self, url):
        filename = os.path.basename(url)
        r=requests.get(url)
        with open(filename,"wb") as f:
             f.write(r.content)
        f.close()

    # 启动方法
    def go(self):
        htmls = self.__fetch_content()
        anchors = self.__getcourseurl(htmls)
        self.__show(anchors)

t = CourseSpider()
t.go()

 



