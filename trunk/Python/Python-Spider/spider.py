
# 引入python自带的http请求库
from urllib import request
import re

# mock http request 
# 正则\s\S匹配所有字符 *代表匹配0次或无线多次 ?代表非贪婪模式 () 代表组 正则匹配之后只取到需要的数据
class Spider():
    url = 'https://www.panda.tv/cate/lol'
    # 匹配所有符合的大的节点
    VIDEOINFO = '<div class="video-info">([\s\S]*?)</div>'
    # 匹配名称
    VIDEONAME = '<span class="video-nickname" title="([\s\S]*?)">'
    # 匹配观看人数
    VIDEONUMBER = '<span class="video-number">([\s\S]*?)</span>'

    def __fetch_content(self):
        r = request.urlopen(Spider.url)
        htmls = r.read()
        htmls = str(htmls, encoding='utf-8')
        return htmls
    
    def __analysis(self, htmls):
        root_html = re.findall(Spider.VIDEOINFO, htmls)
        anchors = []
        for html in root_html:
            name = re.findall(Spider.VIDEONAME, html)
            number = re.findall(Spider.VIDEONUMBER, html)
            anchor = {'name': name, 'number': number}
            anchors.append(anchor)
        return anchors

    # 数据精炼 去掉空格和换行符
    def __refine(self, anchors):
        l = lambda anchor: {
          'name': anchor['name'][0].strip(),
          'number': anchor['number'][0]
          }
        return map(l, anchors)
    
    # 对处理的数据进行排序
    # key 是指定用哪个来进行比较
    def __sort(self, anchors):
        anchors = sorted(anchors, key=self.__sort_key)
        return anchors

    # 定义key排序的规则
    def __sort_key(self, anchor):
        r = re.findall('\d*', anchor['number'])
        number = float(r[0])
        if '万' in anchor['number']:
            number *= 10000
        return number

    # 打印排序的结果
    def __show(self, anchors):
        for anchor in anchors:
            print(anchor['name'] + '-------' + anchor['number'])

    # 启动方法
    def go(self):
        htmls = self.__fetch_content()
        anchors = self.__analysis(htmls)
        anchors = list(self.__refine(anchors))
        anchors = self.__sort(anchors)
        self.__show(anchors)

t = Spider()
t.go()
